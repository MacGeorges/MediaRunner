using UnityEngine;
using UnityEngine.UI;

public class ImageInspectorController : VisualElementInspectorPanel
{
    [SerializeField]
    private Dropdown imageTypeDropDown;

    public void Init(VignetteController vignette)
    { 
        targetTransform = vignette.display.transform;
        base.Init();
    }

    public void ImageTypeSelected(int type)
    {
        VignettesManager.Instance.SelectedVignette.display.SetImageType(type);
    }
}
