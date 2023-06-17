using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class CustomElevatorButton : MonoBehaviour
{
    public ElevatorManager elevatorManager;
    public enum Type { MainRoom, Knife, Matters, Helen, Above, Cave, Up, Down }
    public Type type;

    bool pressed = false;

    private void Start()
    {
        if (elevatorManager == null)
            elevatorManager = GameObject.Find("[ElevatorSystem]/Elevator").GetComponent<ElevatorManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (pressed) return;

        if(other.CompareTag("Hand"))
        {
            if(!pressed)
            {
                pressed = true;

                if (type == Type.Up)
                    elevatorManager.GoUp();
                else if (type == Type.Down)
                    elevatorManager.GoDown();
                else
                    elevatorManager.GoTo((int)type);
            }
        }
    }

}
