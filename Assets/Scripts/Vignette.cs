using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class Vignette : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler// required interface when using the OnPointerEnter method.
{
    [SerializeField]
    private Importer importer;

    [SerializeField]
    private Image image;

    //Do this when the cursor enters the rect area of this selectable UI object.
    public void OnPointerEnter(PointerEventData eventData)
    {
        importer.currentVignette = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        importer.currentVignette = null;
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}