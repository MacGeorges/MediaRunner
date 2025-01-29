using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
