using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

public class HumiditySensor : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;
    public TMP_Text serverDataText;
    bool humidityScale = true;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        //Debug.Log(humidityScale);
        serverDataText.text = "Random number form server: " + humidityScale;
    }    

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        humidityScale = (bool)stream.ReceiveNext();
    }

    #endregion
}
