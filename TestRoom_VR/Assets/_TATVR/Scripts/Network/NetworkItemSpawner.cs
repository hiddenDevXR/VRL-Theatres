using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkItemSpawner : MonoBehaviourPunCallbacks
{
    private GameObject spawnedItemPrefab;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        spawnedItemPrefab = PhotonNetwork.Instantiate(NetworkItems.GetCurrentItemName(), transform.position, Quaternion.identity);
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(spawnedItemPrefab);
    }
}
