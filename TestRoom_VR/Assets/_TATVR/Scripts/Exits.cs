using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exits : MonoBehaviour
{
    BoxCollider myCollider;

    void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        StartCoroutine(EnableCollider());
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(8f);
        myCollider.enabled = true;
    }
}
