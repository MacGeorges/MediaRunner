using Klak.Ndi;
using Klak.Ndi.Interop;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(AudioSource))]
public class VignetteDisplayController : MonoBehaviour
{
    [field: SerializeField]
    public Image image { get; private set; }
    [field: SerializeField]
    public RawImage video { get; private set; }

    [field: SerializeField]
    public VideoPlayer videoPlayer { get; private set; }

    [field: SerializeField]
    public NdiReceiver ndiReceiver { get; private set; }

    private CanvasGroup canvasGroup;
    private AudioSource audioSource;

    [SerializeField] //Display for debug
    private VignetteController vignetteRef;

    private float targetAlpha;
    private float alphaSpeed;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

        //TODO: Use a curve for that
        if(Mathf.Abs(targetAlpha - canvasGroup.alpha) > 0)
        {
            canvasGroup.alpha = Mathf.Clamp01(Mathf.Lerp(canvasGroup.alpha, targetAlpha, Time.deltaTime * (alphaSpeed / Mathf.Abs(targetAlpha - canvasGroup.alpha))));
        }

        videoPlayer.SetDirectAudioVolume(0, canvasGroup.alpha);
    }

    public void Initialize(VignetteController vignette, RenderTexture renderTexture)
    {
        vignetteRef = vignette;
        image.gameObject.SetActive(false);
        video.gameObject.SetActive(false);

        switch (vignette.vignetteData.mode)
        {
            case DataType.None:
                break;
            case DataType.Audio:
                audioSource.clip = vignette.audioClip;
                audioSource.playOnAwake = false;
                break;
            case DataType.Image:
                image.gameObject.SetActive(true);
                image.sprite = vignette.sprite;
                break;
            case DataType.Video:
            case DataType.NDI:
                video.gameObject.SetActive(true);
                video.texture = renderTexture;

                videoPlayer.url = vignette.vignetteData.dataPath;
                videoPlayer.targetTexture = renderTexture;
                videoPlayer.playOnAwake = false;

                ndiReceiver.ndiName = vignette.vignetteData.dataPath;
                ndiReceiver.targetTexture = renderTexture;
                break;
        }
    }

    public void Display(bool display, float speed)
    {
        alphaSpeed = speed;
        targetAlpha = display ? 1 : 0;
    }

    public void DisplayMedia()
    {
        Display(false, vignetteRef.vignetteData.transitionSpeed);

        switch (vignetteRef.vignetteData.mode)
        {
            case DataType.None:
                break;
            case DataType.Image:
                DisplayImage(vignetteRef.sprite);
                break;
            case DataType.Video:
                DisplayVideo(vignetteRef.vignetteData.dataPath);
                break;
            case DataType.Audio:
                PlayAudio(vignetteRef.audioClip);
                break;
            case DataType.NDI:
                break;
        }

        Display(true, vignetteRef.vignetteData.transitionSpeed);
    }

    private void DisplayVideo(string videoPath)
    {
        videoPlayer.url = videoPath;
        videoPlayer.playOnAwake = false;
        videoPlayer.prepareCompleted += OnPrepareCompleted;
        videoPlayer.Prepare();
    }

    private void OnPrepareCompleted(VideoPlayer vp)
    {
        videoPlayer.Play();
        Display(true, vignetteRef.vignetteData.transitionSpeed);
    }

    private void DisplayImage(Sprite sprite)
    {
        image.gameObject.SetActive(true);
        image.sprite = sprite;
    }

    private void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
