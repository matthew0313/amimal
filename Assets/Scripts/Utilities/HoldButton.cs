using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldButton : Button
{
    public bool buttonPressed;
    void Update()
    {
        if (IsPressed())
        {
            buttonPressed = true;
        }
        else buttonPressed = false;
    }
}