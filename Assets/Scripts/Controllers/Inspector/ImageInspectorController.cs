using UnityEngine;

public class ImageInspectorController : VisualElementInspectorPanel
{
    private void Start()
    { 
        targetTransform = VignettesManager.Instance.SelectedVignette.display.transform;
        base.Init();
    }
}
