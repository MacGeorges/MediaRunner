using UnityEngine;

public class ImageInspectorController : VisualElementInspectorPanel
{
    public void Init(VignetteController vignette)
    { 
        targetTransform = vignette.display.transform;
        base.Init();
    }
}
