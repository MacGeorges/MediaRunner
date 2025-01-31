using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Theme", menuName = "Scriptable Objects/Theme")]
public class Theme : ScriptableObject
{
    [Header("Vignette")]
    public Sprite DefaultVignette;
    public Sprite VideoVignette;
    public Sprite AudioVignette;
    public Sprite NDIVignette;

    [Header("Video Controls")]
    public Sprite Play;
    public Sprite Pause;
    public Sprite Stop;
    public Sprite Forward;
}
