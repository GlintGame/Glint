using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Luminosity.IO;

public class GamepadInputModule : MonoBehaviour {
    private GameObject currentButton;
    private AxisEventData currentAxis;
    //timer
    public float timeBetweenInputs = 0.15f; //in seconds
    [Range(0, 1)]
    public float deadZone = 0.15f;
    private float timer = 0;

    void Update()
    {
        if (timer <= 0)
        {
            currentAxis = new AxisEventData(EventSystem.current);
            currentButton = EventSystem.current.currentSelectedGameObject;

            if (InputManager.GetAxis("UI_GPVertical") > deadZone) // move up
            {
                currentAxis.moveDir = MoveDirection.Down;
                ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
            }
            else if (InputManager.GetAxis("UI_GPVertical") < -deadZone) // move down
            {
                currentAxis.moveDir = MoveDirection.Up;
                ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
            }
            else if (InputManager.GetAxis("UI_GPHorizontal") > deadZone) // move right
            {
                currentAxis.moveDir = MoveDirection.Left;
                ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
            }
            else if (InputManager.GetAxis("UI_GPHorizontal") < -deadZone) // move left
            {
                currentAxis.moveDir = MoveDirection.Right;
                ExecuteEvents.Execute(currentButton, currentAxis, ExecuteEvents.moveHandler);
            }
            timer = timeBetweenInputs;
        }

        //timer counting down
        timer -= Time.fixedDeltaTime;

    }
}
