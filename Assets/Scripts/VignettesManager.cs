using UnityEngine;

public class VignettesManager : MonoBehaviour
{
    public VignetteController currentVignette;

    public static VignettesManager instance;

    private void Awake()
    {
        instance = this;    
    }
}
