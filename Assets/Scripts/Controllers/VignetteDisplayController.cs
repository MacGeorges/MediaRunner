using Klak.Ndi;
using Klak.Ndi.Interop;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CanvasGroup))]
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
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Initialize(VignetteData data, RenderTexture renderTexture)
    {
        image.gameObject.SetActive(false);
        video.gameObject.SetActive(false);

        animator.SetFloat("Speed", data.transitionSpeed);

        switch (data.mode)
        {
            case DataType.None:
            case DataType.Audio:
                break;
            case DataType.Image:
                image.gameObject.SetActive(true);
                break;
            case DataType.Video:
            case DataType.NDI:
                video.gameObject.SetActive(true);
                video.texture = renderTexture;

                videoPlayer.url = data.dataPath;
                videoPlayer.targetTexture = renderTexture;

                ndiReceiver.ndiName = data.dataPath;
                ndiReceiver.targetTexture = renderTexture;
                break;
        }
    }

    private void Update()
    {
        if (canvasGroup.alpha > 0)
        {
            videoPlayer.SetDirectAudioVolume(0, canvasGroup.alpha);
        }
    }

    public void Display(bool display, float speed)
    {
        //Will fix once the player authority is moved here
        if (speed > 0)
        {
            animator.SetFloat("Speed", speed);
        }

        animator.SetBool("Display", display);
    }
}
