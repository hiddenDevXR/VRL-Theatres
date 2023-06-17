using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthControl : MonoBehaviour
{
    public Material Stone2Material;
    [SerializeField] private float InitDecayHeight;
    [SerializeField] private float GrowthDist;
    [SerializeField] private float GrowthRate;
    private float growthHeight;
    private string decayHeightRef = "Vector1_fa2025e215a74929bd47195ae475d3fd";

    private void Start()
    {
        growthHeight = InitDecayHeight;
    }
    private void FixedUpdate()
    {
        growthHeight += GrowthRate;
        if (growthHeight >= GrowthDist + InitDecayHeight)
            growthHeight = InitDecayHeight;

        Stone2Material.SetFloat(decayHeightRef, growthHeight);
    }
}
