using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Vector3 position;
    
    private void Update()
    {
        position = transform.position;
    }
}
