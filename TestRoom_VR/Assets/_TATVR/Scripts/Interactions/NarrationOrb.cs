using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationOrb : MonoBehaviour
{

    AudioSource audioSource;

    public float radius;

    public static AudioSource currentSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        UpdateVolume();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (currentSource != null)
                currentSource.Stop();
            currentSource = audioSource;
            audioSource.Play();
            GetComponent<SphereCollider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
            
    }

    void UpdateVolume()
    {
        float distance = Vector3.Distance(LevelManager._player.position, transform.position);
        float volume = 1 - (distance / radius);
        audioSource.volume = volume;
    }
}
