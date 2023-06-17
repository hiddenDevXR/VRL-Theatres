using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowElevator : MonoBehaviour
{
    public Transform player;
    float maxVel = 40;
    private bool smoot = true;
    public float smoothSpeed = 0.125f;
    public float minZValue, maxZValue;
    private Transform lookAt;


    private void Start()
    {
        lookAt = player;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(transform.position.x, lookAt.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }

    Vector3 ClampOffSet(Vector3 offSet)
    {
        float magnitude = player.GetComponent<Rigidbody2D>().velocity.magnitude;
        float z = Mathf.Clamp(-minZValue - Mathf.Abs(minZValue * magnitude / maxVel), -maxZValue, -minZValue);
        offSet = new Vector3(offSet.x, offSet.y, z);
        return offSet;
    }
}
