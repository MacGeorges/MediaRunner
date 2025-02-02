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

    private void Start()
    {
        looping.onValueChanged.AddListener(OnLoopingChanged);
        progress.onValueChanged.AddListener(OnProgressChanged);
        playSpeed.onValueChanged.AddListener(OnPlaySpeedChanged);
    }

    public void Init(VignetteController vignette)
    {
        targetTransform = vignette.display.transform;
        base.Init();
    }

    private void Update()
    {
        if (VignettesManager.Instance.SelectedVignette)
        {
            progress.maxValue = (float)VignettesManager.Instance.SelectedVignette.display.videoPlayer.length;
            progress.value = (float)VignettesManager.Instance.SelectedVignette.display.videoPlayer.time;

            playPauseIcon.sprite = VignettesManager.Instance.SelectedVignette.display.videoPlayer.isPlaying ?
                                ThemeManager.Instance.GetTheme().Pause : ThemeManager.Instance.GetTheme().Play;
        }
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
        if(VignettesManager.Instance.SelectedVignette.display.videoPlayer.isPlaying)
        {
            VignettesManager.Instance.SelectedVignette.display.videoPlayer.Pause();
            playPauseIcon.sprite = ThemeManager.Instance.GetTheme().Play;
        }
        else
        {
            VignettesManager.Instance.SelectedVignette.display.videoPlayer.Play();
            playPauseIcon.sprite = ThemeManager.Instance.GetTheme().Pause;
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
