using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(AudioSource))]
public class PresentationManager : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private RawImage video;

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

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
        videoPlayer.Play();
    }

    public void PlayAudio(AudioClip audioClip)
    {
        ClearPresentation();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
