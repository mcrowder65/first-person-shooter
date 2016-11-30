using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class Utilities
{
    private static Vector3 initialPosition = new Vector3(4.4f, 53.2f, -70f);
    public static bool isXboxController()
    {
        return Input.GetJoystickNames().Length > 0 && Input.GetJoystickNames()[0].ToString() == "Controller (XBOX 360 For Windows)";
    }
    public static Vector3 getNewRespawnPoint()
    {
        //TODO this is where we can add respawns
        return initialPosition;
    }
}
