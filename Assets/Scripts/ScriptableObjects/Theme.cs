using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Theme", menuName = "Scriptable Objects/Theme")]
public class Theme : ScriptableObject
{
    public Sprite DefaultVignette;
    public Sprite VideoVignette;
    public Sprite AudioVignette;
    public Sprite NDIVignette;
}
