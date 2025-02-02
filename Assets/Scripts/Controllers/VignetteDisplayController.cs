using Klak.Ndi;
using Klak.Ndi.Interop;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(RawImage))]
[RequireComponent(typeof(AspectRatioFitter))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(NdiReceiver))]
[RequireComponent(typeof(AudioSource))]
public class VignetteDisplayController : MonoBehaviour
{
    private RawImage image;
    private AspectRatioFitter aspectRatioFitter;
    private Mask mask;
    public VideoPlayer videoPlayer { get; private set; }
    private NdiReceiver ndiReceiver;
    private AudioSource audioSource;

    [SerializeField] //Display for debug
    private VignetteController vignetteRef;

    private bool targetDisplayHide;
    private float alphaSpeed;
    private float displayHideEvaluateTime;

    private void Awake()
    {
        image = GetComponent<RawImage>();
        aspectRatioFitter = GetComponent<AspectRatioFitter>();
        mask = GetComponent<Mask>();
        videoPlayer = GetComponent<VideoPlayer>();
        ndiReceiver = GetComponent<NdiReceiver>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        displayHideEvaluateTime = Mathf.Clamp01(targetDisplayHide ? displayHideEvaluateTime += (Time.deltaTime * alphaSpeed) : displayHideEvaluateTime -= (Time.deltaTime * alphaSpeed));
        
        Color updatedColor = image.color;
        updatedColor.a = vignetteRef.vignetteData.transitionCurve.Evaluate(displayHideEvaluateTime);
        image.color = updatedColor;

        videoPlayer.SetDirectAudioVolume(0, image.color.a);
    }

    public void Initialize(VignetteController vignette, RenderTexture renderTexture)
    {
        vignetteRef = vignette;

        switch (vignette.vignetteData.mode)
        {
            case DataType.None:
                break;
            case DataType.Audio:
                image.enabled = false;
                audioSource.clip = vignette.audioClip;
                audioSource.playOnAwake = false;
                break;
            case DataType.Image:
                image.enabled = true;
                image.texture = vignette.sprite.texture;
                break;
            case DataType.Video:
            case DataType.NDI:
                image.texture = renderTexture;

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
        targetDisplayHide = display;
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
        image.texture = sprite.texture;
        aspectRatioFitter.aspectRatio = ((float)sprite.texture.width / (float)sprite.texture.height);
    }

    public void SetImageType(int type)
    {
        mask.enabled = (type == 1);
    }

    private void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
