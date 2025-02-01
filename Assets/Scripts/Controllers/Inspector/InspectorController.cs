using UnityEngine;
using TMPro;
using System.Linq;

public class InspectorController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private TMP_Text vignetteType;
    [SerializeField]
    private TMP_Text vignetteName;

    [Header("Panels")]
    [SerializeField]
    private ImageInspectorController imagePanel;
    [SerializeField]
    private VideoInspectorController videoPanel;
    [SerializeField]
    private AudioInspectorController audioPanel;
    [SerializeField]
    private NDIInspectorController NDIPanel;

    private void Start()
    {
        ClearPanels();
        VignettesManager.Instance.OnSelectedVignetteChanged.AddListener(DisplayVignetteInfo);
    }

    private void ClearPanels()
    {
        imagePanel.gameObject.SetActive(false);
        videoPanel.gameObject.SetActive(false);
        audioPanel.gameObject.SetActive(false);
        NDIPanel.gameObject.SetActive(false);
    }

    private void DisplayVignetteInfo(VignetteController vignette)
    {
        ClearPanels();

        if (!vignette) { return; }

        vignetteType.text = vignette.vignetteData.mode.ToString();
        vignetteName.text = vignette.vignetteData.dataPath.Split("\\").Last();

        switch (vignette.vignetteData.mode)
        {
            case DataType.None:
                break;
            case DataType.Image:
                imagePanel.gameObject.SetActive(true);
                break;
            case DataType.Video:
                videoPanel.gameObject.SetActive(true);
                break;
            case DataType.Audio:
                audioPanel.gameObject.SetActive(true);
                break;
            case DataType.NDI:
                NDIPanel.gameObject.SetActive(true);
                break;
        }
    }
}
