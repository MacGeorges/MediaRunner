using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class VignetteController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler// required interface when using the OnPointerEnter method.
{
    [SerializeField]
    private Image image;

    private Sprite sprite;

    private void Awake()
    {
        sprite = image.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        VignettesManager.instance.currentVignette = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        VignettesManager.instance.currentVignette = null;
    }

    public void SetSprite(Sprite newSprite)
    {
        sprite = newSprite;
        image.sprite = sprite;
    }

    public void OnVignetteClicked()
    {
        PresentationManager.Instance.DisplayVignette(sprite);
    }
}