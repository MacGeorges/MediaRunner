using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;

[RequireComponent(typeof(AudioSource))]
public class PresentationManager : MonoBehaviour
{
    [SerializeField] //Showing for debug
    private VignetteDisplayController vignetteDisplay;

    public AudioSource audioSource
    { get; private set; }

    public static PresentationManager Instance;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    public void DisplaySelectedVignette()
    {
        VignetteController vignette = VignettesManager.Instance.SelectedVignette;
        if (!vignette) { return; }

        vignetteDisplay = vignette.display;

        vignetteDisplay.Display(false, vignette.vignetteData.transitionSpeed);

        switch (vignette.vignetteData.mode)
        {
            case DataType.None:
                break;
            case DataType.Image:
                DisplayImage(vignette.sprite);
                break;
            case DataType.Video:
                DisplayVideo(vignette.vignetteData.dataPath);
                break;
            case DataType.Audio:
                PlayAudio(vignette.audioClip);
                break;
            case DataType.NDI:
                break;
        }

        vignetteDisplay.Display(true, vignette.vignetteData.transitionSpeed);
    }

    private void DisplayImage(Sprite sprite)
    {
        vignetteDisplay.image.gameObject.SetActive(true);
        vignetteDisplay.image.sprite = sprite;
    }

    //TODO: Move that on the VignetteDisplayController
    private void DisplayVideo(string videoPath)
    {
        //vignetteDisplay.videoPlayer.url = videoPath;
        vignetteDisplay.videoPlayer.playOnAwake = false;
        vignetteDisplay.videoPlayer.prepareCompleted += OnPrepareCompleted;
        vignetteDisplay.videoPlayer.Prepare();
    }

    //TODO: Move that on the VignetteDisplayController
    private void OnPrepareCompleted(VideoPlayer vp)
    {
        vignetteDisplay.videoPlayer.Play();
        vignetteDisplay.Display(true, -1);
    }

    private void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
