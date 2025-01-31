using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(AudioSource))]
public class PresentationManager : MonoBehaviour
{
    [field: SerializeField]
    public Image image
    { get; private set; }

    [field: SerializeField]
    public RawImage video
    { get; private set; }

    public VideoPlayer videoPlayer
    { get; private set; }

    public AudioSource audioSource
    { get; private set; }

    public static PresentationManager Instance;

    private void Awake()
    {
        Instance = this;

        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void ClearPresentation()
    {
        image.gameObject.SetActive(false);
        videoPlayer.Stop();
        video.gameObject.SetActive(false);
        audioSource.Stop();
    }

    public void DisplayImage(Sprite sprite)
    {
        ClearPresentation();
        image.gameObject.SetActive(true);
        image.sprite = sprite;
    }

    public void DisplayVideo(string videoPath)
    {
        ClearPresentation();
        video.gameObject.SetActive(true);
        videoPlayer.url = videoPath;
        videoPlayer.playOnAwake = false;
        videoPlayer.prepareCompleted += OnPrepareCompleted;
        videoPlayer.Prepare();
    }

    private void OnPrepareCompleted(VideoPlayer vp)
    {
        videoPlayer.Play();
    }

    public void PlayAudio(AudioClip audioClip)
    {
        ClearPresentation();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
