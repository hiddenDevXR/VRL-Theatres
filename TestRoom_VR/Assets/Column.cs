using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    MeshRenderer mesh;
    public GameObject partner;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void ChaneVisualization()
    {
        mesh.enabled = false;
        partner.SetActive(true);
    }
}
