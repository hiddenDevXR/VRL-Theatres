using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TAT_Hands : MonoBehaviour
{
    public Transform paintPoint;
    public GameObject paint;
    public float maxDistance = 1f;
    
    public enum Side { Left = 1, Right = -1 };
    public Side side;

    HandPrint handPrint;

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(paintPoint.position, transform.up * (int)side, out hit, maxDistance))
        {
            if (hit.transform.gameObject.CompareTag("Wall"))
            {
                Debug.DrawLine(paintPoint.position, hit.point);
                GameObject _print = Instantiate(paint, new Vector3(hit.point.x + 0.0001f, hit.point.y, hit.point.z), Quaternion.LookRotation(hit.normal * -1));
                _print.transform.localEulerAngles = new Vector3(_print.transform.localEulerAngles.x, _print.transform.localEulerAngles.y, -transform.eulerAngles.x);
                handPrint = _print.GetComponent<HandPrint>();
            }

            else if(hit.transform.gameObject.CompareTag("HandPrint"))
            {
                handPrint.pressed = true;
            }

            if (hit.transform.gameObject.name == "Column")
            {
                Column column = hit.transform.gameObject.GetComponent<Column>();
                if(column != null)
                     column.ChaneVisualization();
            }
        }
    }
}
