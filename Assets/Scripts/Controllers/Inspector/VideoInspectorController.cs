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
    [SerializeField]
    private Slider playSpeed;
    [SerializeField]
    private TMP_Text playSpeedValue;

    [SerializeField]
    private Image playPauseIcon;
    [SerializeField]
    private Image stopIcon;

    private bool isPlaying;

    private void Start()
    {
        targetTransform = PresentationManager.Instance.video.transform;
        base.Init();

        looping.onValueChanged.AddListener(OnLoopingChanged);
        progress.onValueChanged.AddListener(OnProgressChanged);
        playSpeed.onValueChanged.AddListener(OnPlaySpeedChanged);
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

    private void OnPlaySpeedChanged(float newSpeed)
    {
        PresentationManager.Instance.videoPlayer.playbackSpeed = newSpeed;
        playSpeedValue.text = "x" + newSpeed;
    }

    private void OnLoopingChanged(bool newLoopingValue)
    {
        PresentationManager.Instance.videoPlayer.isLooping = newLoopingValue;
    }

    public void PlayPauseButtonPressed()
    {
        if(isPlaying)
        {
            PresentationManager.Instance.videoPlayer.Pause();
            playPauseIcon.sprite = ThemeManager.Instance.GetTheme().Play;
            isPlaying = false;
        }
        else
        {
            PresentationManager.Instance.videoPlayer.Play();
            playPauseIcon.sprite = ThemeManager.Instance.GetTheme().Pause;
            isPlaying = true;
        }
    }

    public void StopButtonPressed()
    {
        PresentationManager.Instance.videoPlayer.Stop();
    }

    public void ForwardButtonPressed()
    {
        PresentationManager.Instance.videoPlayer.StepForward();
    }
}
