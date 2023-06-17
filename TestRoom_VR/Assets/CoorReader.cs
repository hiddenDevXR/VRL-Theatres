using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoorReader : MonoBehaviour
{

    public TMP_Text x, y;

    public float posx, posy;

    public void Update()
    {
        x.text = posx.ToString();
        y.text = posy.ToString();
    }
}
