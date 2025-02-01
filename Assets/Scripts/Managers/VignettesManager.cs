using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;

public class VignettesManager : MonoBehaviour
{

    [SerializeField]
    private VignetteDisplayController vignetteDisplayPrefab;
    [SerializeField]
    private Transform vignettesDisplayRoot;

    [SerializeField]
    private VignetteController vignettePrefab;
    [SerializeField]
    private RowController rowPrefab;
    [SerializeField]
    private Transform rowRoot;

    public UnityEvent<VignetteController> OnSelectedVignetteChanged;

    public VignetteController HoveredVignette;
    [field: SerializeField]
    public VignetteController SelectedVignette {  get; private set; }

    public static VignettesManager Instance;

    private void Awake()
    {
        Instance = this;    
    }

    private void Start()
    {
        PopulateUI(SaveManager.Instance.GetLayout());
    }

    private void PopulateUI(Layout layout)
    {
        foreach(Row row in layout.Rows)
        {
            RowController newRow = Instantiate(rowPrefab, rowRoot);
            foreach(VignetteData vignette in row.vignettes)
            {
                VignetteController newVignette = Instantiate(vignettePrefab, newRow.vignettesRoot);
                VignetteDisplayController newVignetteDisplay = Instantiate(vignetteDisplayPrefab, vignettesDisplayRoot);

                RenderTexture renderTexture;
                renderTexture = new RenderTexture(1920, 1080, 32, RenderTextureFormat.ARGB32);
                renderTexture.Create();

                newVignette.Initialize(vignette, renderTexture, newVignetteDisplay);
            }
        }
    }

    public void SetSelectedVignette(VignetteController newVignette)
    {
        SelectedVignette = newVignette;
        OnSelectedVignetteChanged.Invoke(SelectedVignette);
    }

    public void CreateVignette(Vector2 coordinates)
    {

    }
}
