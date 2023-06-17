using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class XR_Rig : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    public OVRCameraRig CameraRig;
    Rigidbody rb;
    AudioSource audioSource;

    float resetTimer = 0f;

    static int lastScene;
    static int currentScene;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        //ResetPosition();
    }


    public void ResetPosition()
    {
        Vector3 offSet = new Vector3(CameraRig.centerEyeAnchor.transform.position.x - (-4.374864f), 0f, CameraRig.centerEyeAnchor.transform.position.z - (-4.375114f));
        Vector3 targetPosition = transform.position - offSet;

        if (Input.GetButton("Fire1"))
        {
            resetTimer += Time.deltaTime;
            if (resetTimer >= 2f)
            {
                resetTimer = 0;
                rb.MovePosition(targetPosition);
                audioSource.Play();
            }
        }

        else if (Input.GetButtonUp("Fire1"))
        {
            resetTimer = 0;
        }
    }
}
