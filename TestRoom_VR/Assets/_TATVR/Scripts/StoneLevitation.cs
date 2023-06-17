using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneLevitation : MonoBehaviour
{
    [SerializeField] float levitationSpeed;
    [SerializeField] float amplitude;
    [SerializeField] float turningSpeed;
    [SerializeField] float turningAmplitude;
    private Transform m_transform;
    private float initialY;
    private Vector3 initialAng;
    private float timeOffset; //Add variation to each stone
    void Start()
    {
        m_transform = GetComponent<Transform>();
        initialY = m_transform.position.y;
        initialAng = m_transform.localEulerAngles;
        timeOffset = Random.Range(0f, 5f);
    }

    void FixedUpdate()
    {
        //sets position and rotation using cos function
        Vector3 pos = new Vector3(m_transform.position.x, 
            initialY + Mathf.Sin(Time.time + timeOffset) * levitationSpeed * amplitude, 
            m_transform.position.z);
        Vector3 rot = new Vector3(Mathf.Sin(Time.time + 8f + timeOffset) * turningSpeed * turningAmplitude * 2f, 
            Mathf.Sin(Time.time + timeOffset) * turningSpeed * turningAmplitude, 
            Mathf.Sin(Time.time + 0.2f + timeOffset) * turningSpeed * turningAmplitude * 0.5f) + initialAng;
        m_transform.position = pos;
        m_transform.localEulerAngles = rot;
    }
}