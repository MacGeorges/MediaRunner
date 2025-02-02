using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class PresentationManager : MonoBehaviour
{
    public static PresentationManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
