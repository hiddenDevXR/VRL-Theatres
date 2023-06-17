using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Transform node;

    private void Awake()
    {
        if(node == null)
            node = transform;
    }
}
