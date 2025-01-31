using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;

public struct VignetteData
{
    public DataType mode;
    public string dataPath;
}

public class VignettesManager : MonoBehaviour
{
    [SerializeField]
    private VignetteController vignettePrefab;
    [SerializeField]
    private RowController rowPrefab;

    public UnityEvent<VignetteController> OnSelectedVignetteChanged;

    public VignetteController HoveredVignette;
    private VignetteController SelectedVignette;

    public static VignettesManager Instance;

    private void Awake()
    {
        Instance = this;    
    }

    public void SetSelectedVignette(VignetteController newVignette)
    {
        SelectedVignette = newVignette;
        OnSelectedVignetteChanged.Invoke(SelectedVignette);
    }
}
