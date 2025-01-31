using UnityEngine;

public class ImageInspectorController : VisualElementInspectorPanel
{
    private void Start()
    {
        targetTransform = PresentationManager.Instance.image.transform;
        base.Init();
    }
}
