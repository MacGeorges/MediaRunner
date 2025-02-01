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

    public void Init()
    {
        targetTransform = VignettesManager.Instance.SelectedVignette.display.transform;
        base.Init();

        looping.onValueChanged.AddListener(OnLoopingChanged);
        progress.onValueChanged.AddListener(OnProgressChanged);
        playSpeed.onValueChanged.AddListener(OnPlaySpeedChanged);
    }

    private void Update()
    {
        progress.maxValue = (float)VignettesManager.Instance.SelectedVignette?.display.videoPlayer.length;
        progress.value = (float)VignettesManager.Instance.SelectedVignette?.display.videoPlayer.time;
    }

    private void OnProgressChanged(float newTime)
    {
        VignettesManager.Instance.SelectedVignette.display.videoPlayer.time = newTime;
    }

    private void OnPlaySpeedChanged(float newSpeed)
    {
        VignettesManager.Instance.SelectedVignette.display.videoPlayer.playbackSpeed = newSpeed;
        playSpeedValue.text = "x" + newSpeed;
    }

    private void OnLoopingChanged(bool newLoopingValue)
    {
        VignettesManager.Instance.SelectedVignette.display.videoPlayer.isLooping = newLoopingValue;
    }

    public void PlayPauseButtonPressed()
    {
        if(isPlaying)
        {
            VignettesManager.Instance.SelectedVignette.display.videoPlayer.Pause();
            playPauseIcon.sprite = ThemeManager.Instance.GetTheme().Play;
            isPlaying = false;
        }
        else
        {
            VignettesManager.Instance.SelectedVignette.display.videoPlayer.Play();
            playPauseIcon.sprite = ThemeManager.Instance.GetTheme().Pause;
            isPlaying = true;
        }
    }

    public void StopButtonPressed()
    {
        VignettesManager.Instance.SelectedVignette.display.videoPlayer.Stop();
    }

    public void ForwardButtonPressed()
    {
        VignettesManager.Instance.SelectedVignette.display.videoPlayer.StepForward();
    }
}
