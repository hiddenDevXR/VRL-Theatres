using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkEntity : MonoBehaviour
{
    private PhotonView photonView;
    //public Entity entity;
    public string objectName;

    bool enableTracking = false;

    public GameObject relatedObject;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        GameObject instance = Instantiate(relatedObject, transform.position, Quaternion.identity);            
    }

    void Update()
    {
        //if (photonView.IsMine && enableTracking)
        //{
        //    MapPosition(entity.node);
        //}
    }

    //void MapPosition(Transform node)
    //{
    //    //transform.position = node.position;
    //    //transform.rotation = node.rotation;
    //}

    //IEnumerator FindEntity()
    //{
    //    while (entity == null)
    //    {
    //        entity = GameObject.Find(objectName).GetComponent<Entity>();
    //        yield return null;
    //    }

    //    enableTracking = true;
    //}
}
