using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveTest : MonoBehaviour
{
    public Transform target;
    public float speed = 0;

    public float step = 0;
    public float timeToArrive = 0;

    void Update()
    {      
        speed += step;
        timeToArrive += Time.deltaTime;
        
        if (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed);   
            Debug.Log("Speed: " + speed);
            Debug.Log("Time: " + timeToArrive);
        }
   
    }
}
