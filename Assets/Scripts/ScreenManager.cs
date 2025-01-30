using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class ScreenManager : MonoBehaviour
{

    public static ScreenManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

        Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] is the primary, default display and is always ON, so start at index 1.
        // Check if additional displays are available and activate each.

        Debug.Log("display 0: " + Display.displays[0].active);

        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
            Debug.Log("display resolution: " + Display.displays[i].systemWidth + "x" + Display.displays[i].systemHeight);
        }
    }

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern bool SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
    [DllImport("user32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(System.String className, System.String windowName);

    public static void SetPosition(int x, int y, int resX = 0, int resY = 0)
    {
        SetWindowPos(FindWindow(null, "MediaRunner"), 0, x, y, resX, resY, resX * resY == 0 ? 1 : 0);
    }
#endif
}
