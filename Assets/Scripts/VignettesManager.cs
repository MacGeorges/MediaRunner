using UnityEngine;

public struct VignetteData
{
    public DataType mode;
    public string dataPath;
}

public class VignettesManager : MonoBehaviour
{
    public VignetteController currentVignette;

    public static VignettesManager instance;

    private void Awake()
    {
        instance = this;    
    }
}
