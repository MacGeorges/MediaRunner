using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class VignetteController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler// required interface when using the OnPointerEnter method.
{
    [SerializeField]
    private Image image;

    public VignetteData vignetteData
    {
        get;
        private set;
    }

    private Sprite sprite;
    private AudioClip audioClip;

    private void Awake()
    {
        sprite = image.sprite;
    }

    public void Initialize(VignetteData newVignetteData)
    {
        vignetteData = newVignetteData;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        VignettesManager.Instance.HoveredVignette = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        VignettesManager.Instance.HoveredVignette = null;
    }

    public void SetResource(DataType dataType, string path)
    {
        vignetteData = new VignetteData()
        {
            mode = dataType,
            dataPath = path
        };

        DisplayResource();
    }

    private void DisplayResource()
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
        VignettesManager.Instance.SetSelectedVignette(this);

        switch (vignetteData.mode)
        {
            case DataType.None:
            case DataType.NDI:
                break;
            case DataType.Image:
                PresentationManager.Instance.DisplayImage(sprite);
                break;
            case DataType.Video:
                PresentationManager.Instance.DisplayVideo(vignetteData.dataPath);
                break;
            case DataType.Audio:
                PresentationManager.Instance.PlayAudio(audioClip);
                break;
        }
    }
}