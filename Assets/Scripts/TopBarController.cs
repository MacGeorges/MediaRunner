using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TopBarController : MonoBehaviour, IDragHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        return;
        Debug.Log("Dragging Window: " + eventData.scrollDelta.x + "/" + eventData.scrollDelta.y);

        Vector2 currentScreenPosition = Screen.mainWindowPosition;

        StartCoroutine(MoveWindow(Screen.mainWindowDisplayInfo, currentScreenPosition + eventData.delta));
    }

    IEnumerator MoveWindow(DisplayInfo displayInfo, Vector2 newPosition)
    {
        AsyncOperation moveOperation = Screen.MoveMainWindowTo(displayInfo, Vector2Int.RoundToInt(newPosition));
        yield return moveOperation;
    }
}
