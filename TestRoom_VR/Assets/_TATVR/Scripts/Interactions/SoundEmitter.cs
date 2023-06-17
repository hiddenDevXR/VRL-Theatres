using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    AudioSource audioSource;
    public float soundRadius = 1f;
    public enum EmmiterType { Dynamic, Always_Playing }
    public EmmiterType emmiterType;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position);

        if(emmiterType == EmmiterType.Dynamic)
        {
            if (distance < soundRadius && !audioSource.isPlaying)
            {
                audioSource.Play();
            }

            else if(distance > soundRadius && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        else if(emmiterType == EmmiterType.Always_Playing)
        {
            audioSource.volume = 1 - distance / soundRadius;
        }
        
    }
}
