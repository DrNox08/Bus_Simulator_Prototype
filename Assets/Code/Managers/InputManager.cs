using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class InputManager 
{
    public static ActionMap actionMap;

    static InputManager ()
    {
        actionMap = new ActionMap ();
        actionMap.Enable();
    }

    public static bool PressedW => actionMap.Player.W.IsPressed();
    public static bool PressedA => actionMap.Player.A.IsPressed();
    public static bool PressedD => actionMap.Player.D.IsPressed();
    public static bool PressedSTOP => actionMap.Player.SPACEBAR.IsPressed();

}
