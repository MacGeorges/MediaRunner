using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;

public class ScreenManager : MonoBehaviour
{

    public static ScreenManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;

        // Display.displays[0] is the primary, default display and is always ON, so start at index 1.
        // Check if additional displays are available and activate each.


        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }
}
