using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkItems : MonoBehaviour
{

    public string[] itemNames;
    static string[] _itemNames;
    static int index = 0;


    private void Start()
    {
        _itemNames = itemNames;
    }

    public static string GetCurrentItemName()
    {
        return _itemNames[index++];
    }
}
