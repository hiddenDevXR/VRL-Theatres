using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public GameObject person;
    private PhotonView photonView;
    public XR_Rig xr_rig;

    private void Start()
    {      
        photonView = GetComponent<PhotonView>();
        xr_rig = GameObject.Find("XR_Rig").GetComponent<XR_Rig>();
    }

    void Update()
    {
        if(photonView.IsMine)
        {
            person.SetActive(false);
            MapPosition(head, xr_rig.head);
        }
    }

    void MapPosition(Transform target, Transform node)
    {
        target.position = node.position;
        target.localEulerAngles = new Vector3(0, xr_rig.head.localEulerAngles.y, 0);
        target.localScale = node.localScale;
    }
}
