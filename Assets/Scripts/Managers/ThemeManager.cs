using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    [SerializeField]
    private Theme theme;

    public static ThemeManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Theme GetTheme()
    {
        return theme;
    }
}
