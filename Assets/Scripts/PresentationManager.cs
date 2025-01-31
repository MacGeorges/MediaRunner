using UnityEngine;
using UnityEngine.UI;

public class PresentationManager : MonoBehaviour
{
    [SerializeField]
    private Image presentation;


    public static PresentationManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void DisplayVignette(Sprite spriteToDisplay)
    {
        presentation.sprite = spriteToDisplay;
    }
}
