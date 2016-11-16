using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

static class Utilities
{
    public static bool isXboxController()
    {
        return Input.GetJoystickNames()[0].ToString() == "Controller (XBOX 360 For Windows)";
    }
}
