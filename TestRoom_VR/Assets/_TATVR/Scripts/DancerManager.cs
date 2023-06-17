using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DancerManager : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject[] dancers;
    public static Vector3 offsetPosition;
    private PhotonView photonView;
    private ActorNetworkEntity currentActor;
    private ActorNetworkEntity lastActor;
    public Animator fadeSphereAnimator;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        offsetPosition = transform.position;
        currentActor = dancers[0].GetComponent<ActorNetworkEntity>();
    }

    [PunRPC]
    void SetCalibrationPoint()
    {
        levelManager.rotationPoint.transform.position = new Vector3(currentActor.bodyBones[0].transform.position.x, 0, currentActor.bodyBones[0].transform.position.z);
        levelManager.ParentToPivot();
    }

    [PunRPC]
    void FreeFromCalibrationPoint()
    {
        levelManager.UnparentFromPivot();
        levelManager.rotationPoint.transform.position = Vector3.zero;
    }

    [PunRPC]
    void RotateLevel(float angle)
    {
        levelManager.rotationPoint.eulerAngles += Vector3.up * angle;
    }

    [PunRPC]
    void ChangeDancer(int index)
    {
        PhotonNetwork.RemoveBufferedRPCs();

        lastActor = currentActor;
        currentActor = dancers[index].GetComponent<ActorNetworkEntity>();

        lastActor.gateIsOpen = false;
        currentActor.gameObject.SetActive(true);

        StartCoroutine(ActivateSkinnedMesh());
    }

    [PunRPC]
    public void HideScenery(bool state)
    {
        levelManager.EnableMesh(state);
    }

    IEnumerator DelayFade()
    {       
        yield return new WaitForSeconds(10f);
        PhotonNetwork.Disconnect();
        fadeSphereAnimator.SetBool("fade", true);
        yield return new WaitForSeconds(15f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    [PunRPC]
    public void DissolveScenery(bool state)
    {
        levelManager.Disolve();
        StartCoroutine(DelayFade());   
    }

    public AudioSource audioSource;

    [PunRPC]
    void PlayAudioClip(int index)
    {
        audioSource.Play();
    }

    IEnumerator ActivateSkinnedMesh()
    {
        currentActor.gateIsOpen = true;
        yield return new WaitForSeconds(1f);
        lastActor.ManageMesh(false);
        yield return new WaitForSeconds(2f);
        currentActor.ManageMesh(true);
        currentActor.gateIsOpen = true;
    }    
}
