using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class VignetteController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler// required interface when using the OnPointerEnter method.
{
    [SerializeField]
    private Image image;

    private VignetteData vignetteData;

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
        VignettesManager.instance.currentVignette = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        VignettesManager.instance.currentVignette = null;
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
            case DataType.NDI:
            case DataType.Video:
                break;
            case DataType.Image:
                SetSprite(Importer.Instance.imageImporter.ImportImage(vignetteData.dataPath));
                break;
            case DataType.Audio:
                Importer.Instance.audioImporter.ImportAudio(vignetteData.dataPath, SetAudioClip);
                break ;
        }
    }

    private void SetSprite(Sprite newSprite)
    {
        sprite = newSprite;
        image.sprite = sprite;
    }

    private void SetAudioClip(AudioClip newAudioClip)
    {
        audioClip = newAudioClip;
        image.sprite = null;
    }

    public void OnVignetteClicked()
    {
        switch(vignetteData.mode)
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