using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveGeometry : MonoBehaviour
{
    public Material material;
    float step = 15f;

    void Update()
    {
        step -= Time.deltaTime;
        material.SetFloat("_Cutoff", step);
    }
}
