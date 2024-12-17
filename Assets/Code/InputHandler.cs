using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    private void OnEnable()
    {
        InputManager.actionMap.Player.A.started += Pressed_A;
        InputManager.actionMap.Player.D.started += Pressed_D;
        InputManager.actionMap.Player.W.started += Pressed_W;
        InputManager.actionMap.Player.SPACEBAR.started += Pressed_SpaceBar;
    }
    private void OnDisable()
    {
        InputManager.actionMap.Player.A.started -= Pressed_A;
        InputManager.actionMap.Player.D.started -= Pressed_D;
        InputManager.actionMap.Player.W.started -= Pressed_W;
        InputManager.actionMap.Player.SPACEBAR.started -= Pressed_SpaceBar;
    }

    void Pressed_A(InputAction.CallbackContext context)
    {
        if (UI_Manager.instance.IsButtonActive("LEFT"))
            UI_Manager.instance.DirectionButtonPressed("LEFT");
    }

    void Pressed_D(InputAction.CallbackContext context)
    {
        if (UI_Manager.instance.IsButtonActive("RIGHT"))
            UI_Manager.instance.DirectionButtonPressed("RIGHT");
    }

    void Pressed_W(InputAction.CallbackContext context)
    {
        if (UI_Manager.instance.IsButtonActive("UP"))
            UI_Manager.instance.DirectionButtonPressed("STRAIGHT");
    }

    void Pressed_SpaceBar(InputAction.CallbackContext context)
    {
        if (UI_Manager.instance.IsButtonActive("STOP"))
            UI_Manager.instance.StopButtonPressed();
    }



}
        
        