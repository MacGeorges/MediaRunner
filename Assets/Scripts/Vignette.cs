using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class Vignette : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler// required interface when using the OnPointerEnter method.
{
    [SerializeField]
    private ImageImporter imageExample;

    [SerializeField]
    private Image image;

    //Do this when the cursor enters the rect area of this selectable UI object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        imageExample.currentVignette = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imageExample.currentVignette = null;
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}