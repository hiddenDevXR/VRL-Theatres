using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private string roomName = "DanceFloor";

    public int sceneIndex;
    public GameObject voidWall;

    private GameObject spawnedPlayerPrefab;

    private int playerIndex = 0;

    public PhotonView myPhotonView;

    public GameObject indicator;

    void Start()
    {
        ConnectedToServer();
    }

    private void ConnectedToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect to Server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server.");
        base.OnConnectedToMaster();
        EnterRoom();
    }

    public void EnterRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 0;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        // roomOptions.PlayerTtl = 60000;
        // roomOptions.EmptyRoomTtl = 60000;

        PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
    }

    [PunRPC]
    void RotateLevel(float angle)
    {

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        PhotonNetwork.RemoveBufferedRPCs();

        playerIndex = PhotonNetwork.LocalPlayer.ActorNumber;

        Debug.Log("my player index: " + playerIndex);
        PhotonNetwork.NickName = playerIndex.ToString();

        if (indicator != null)
            indicator.SetActive(true);

        if(sceneIndex == 1)
            spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player", transform.position, Quaternion.identity);

        if (sceneIndex == 2)
        {
            spawnedPlayerPrefab = PhotonNetwork.Instantiate("Network Player_Dance", transform.position, Quaternion.identity);
            AknowledgeToServer();
        }
           
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("A new player joined the Room");
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("A player left the Room");
        base.OnPlayerLeftRoom(otherPlayer);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedPlayerPrefab);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("NetworkManager connection lost...");
        if (CanRecoverFromDisconnect(cause))
        {
            StartCoroutine(Recover());
        }
        else
        {
            Debug.Log(cause);
        }
    }

    private bool CanRecoverFromDisconnect(DisconnectCause cause)
    {
        switch (cause)
        {
            // the list here may be non exhaustive and is subject to review
            case DisconnectCause.Exception:
            case DisconnectCause.ServerTimeout:
            case DisconnectCause.ClientTimeout:
            case DisconnectCause.DisconnectByServerLogic:
            case DisconnectCause.DisconnectByServerReasonUnknown:
                return true;
        }
        return false;
    }

    private IEnumerator Recover()
    {
        while (PhotonNetwork.CurrentRoom == null)
        {
            if (!PhotonNetwork.ReconnectAndRejoin())
            {
                // Debug.LogError("ReconnectAndRejoin failed, trying Reconnect");
                Debug.Log("ReconnectAndRejoin failed, trying Enter room as new user");

                EnterRoom();

                // if (!PhotonNetwork.Reconnect())
                // {
                //     Debug.LogError("Reconnect failed, trying ConnectUsingSettings");
                //     if (!PhotonNetwork.ConnectUsingSettings())
                //     {
                //         Debug.LogError("ConnectUsingSettings failed");
                //     }
                // } else if (!PhotonNetwork.RejoinRoom(roomName))
                // {
                //     Debug.LogError("RejoinRoom failed");
                // }
            }

            yield return new WaitForSeconds(2);
        }
    }

    public void AknowledgeToServer()
    {
        myPhotonView.RPC("AknowledgeUser", RpcTarget.Others, playerIndex);
    }

    public void UserArrived()
    {
        myPhotonView.RPC("PlayPingSFX", RpcTarget.Others);
    }    

    [PunRPC]
    void EnableVoidWall(int index)
    {
        if(sceneIndex == 1)
        {
            Debug.Log("Receiving Data: " + index);
            if (playerIndex == index)
                voidWall.SetActive(true);
        }  
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
    }
}
