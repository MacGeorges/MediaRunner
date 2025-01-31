using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoInspectorController : VisualElementInspectorPanel
{
    [Header("UI Elements")]
    [SerializeField]
    private Toggle looping;
    [SerializeField]
    private Slider progress;

    private void Start()
    {
        targetTransform = PresentationManager.Instance.video.transform;
        base.Init();

        looping.onValueChanged.AddListener(OnLoopingChanged);
        progress.onValueChanged.AddListener(OnProgressChanged);
    }

    private void Update()
    {
        progress.maxValue = (float)PresentationManager.Instance.videoPlayer.length;
        progress.value = (float)PresentationManager.Instance.videoPlayer.time;
    }

    private void OnProgressChanged(float newTime)
    {
        PresentationManager.Instance.videoPlayer.time = newTime;
    }

    private void OnLoopingChanged(bool newLoopingValue)
    {
        PresentationManager.Instance.videoPlayer.isLooping = newLoopingValue;
    }
}
