using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class VignetteController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler// required interface when using the OnPointerEnter method.
{
    [SerializeField]
    private Image image;

    [field: SerializeField]
    public VignetteDisplayController display
    { get; private set; }

    [field: SerializeField] //Displayed for debug
    public VignetteData vignetteData { get; private set; }

    public Sprite sprite { get; private set; }
    public AudioClip audioClip { get; private set; }

    private void Awake()
    {
        sprite = image.sprite;
    }

    public void Initialize(VignetteData newVignetteData, RenderTexture renderTexture, VignetteDisplayController newDisplay)
    {
        vignetteData = newVignetteData;
        display = newDisplay;

        display.Initialize(this, renderTexture);

        DisplayResource();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        VignettesManager.Instance.HoveredVignette = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        VignettesManager.Instance.HoveredVignette = null;
    }

    public void DisplayResource()
    {
        switch (vignetteData.mode)
        {
            case DataType.None:
                SetEmpty();
            break;
            case DataType.Image:
                SetSprite(Importer.Instance.imageImporter.ImportImage(vignetteData.dataPath));
                break;
            case DataType.Video:
                SetVideo();
                break;
            case DataType.Audio:
                Importer.Instance.audioImporter.ImportAudio(vignetteData.dataPath, SetAudioClip);
                break ;
            case DataType.NDI:
                SetNDI();
                break;
        }
    }

    private void SetEmpty()
    {
        sprite = ThemeManager.Instance.GetTheme().DefaultVignette;
        image.sprite = sprite;
    }

    private void SetSprite(Sprite newSprite)
    {
        sprite = newSprite;
        image.sprite = sprite;
    }

    private void SetVideo()
    {
        sprite = null;
        image.sprite = ThemeManager.Instance.GetTheme().VideoVignette;
    }

    private void SetAudioClip(AudioClip newAudioClip)
    {
        audioClip = newAudioClip;
        image.sprite = ThemeManager.Instance.GetTheme().AudioVignette;
    }

    private void SetNDI()
    {
        sprite = null;
        image.sprite = ThemeManager.Instance.GetTheme().NDIVignette;
    }

    public void OnVignetteClicked()
    {
        if (VignettesManager.Instance.SelectedVignette == this)
        {
            VignettesManager.Instance.SetSelectedVignette(null);
            display.Display(false, vignetteData.transitionSpeed);
        }
        else
        {
            VignettesManager.Instance.SetSelectedVignette(this);
            display.DisplayMedia();
        }
    }
}