using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform player;
    public static Transform _player;

    public Transform rotationPoint;
    public GameObject levelMesh;
    public GameObject fakeLevelMesh;
    public AudioSource audioSource;

    private void Start()
    {
        _player = player;
    }

    public void ParentToPivot()
    {
        if (rotationPoint == null) return;

        player.SetParent(transform);
        transform.SetParent(rotationPoint);
    }

    public void UnparentFromPivot()
    {
        player.SetParent(null);
        transform.SetParent(null);
    }

    public void EnableMesh(bool state)
    {
        levelMesh.SetActive(state);
    }

    public void Disolve()
    {
        levelMesh.SetActive(false);
        fakeLevelMesh.SetActive(true);
        audioSource.Play();
    }
}
