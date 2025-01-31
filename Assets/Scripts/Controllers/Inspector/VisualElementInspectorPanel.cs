using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class VisualElementInspectorPanel : MonoBehaviour
{
    protected Transform targetTransform;

    [Header("UI Elements")]
    [SerializeField]
    private Vector3UIElement position;
    [SerializeField]
    private Vector3UIElement rotation;
    [SerializeField]
    private Vector3UIElement scale;

    protected void Init()
    {
        Debug.Log("START", gameObject);

        position.Init(targetTransform.transform.localPosition);
        rotation.Init(targetTransform.transform.localRotation.eulerAngles);
        scale.Init(targetTransform.transform.localScale);

        position.OnValueUpdated.AddListener(OnPositionValueUpdated);
        rotation.OnValueUpdated.AddListener(OnRotationValueUpdated);
        scale.OnValueUpdated.AddListener(OnScaleValueUpdated);
    }

    private void OnPositionValueUpdated(Vector3 newPosition)
    {
        targetTransform.localPosition = newPosition;
    }

    private void OnRotationValueUpdated(Vector3 newRotation)
    {
        targetTransform.localRotation = Quaternion.Euler(newRotation);
    }

    private void OnScaleValueUpdated(Vector3 newScale)
    {
        targetTransform.localScale = newScale;
    }
}
