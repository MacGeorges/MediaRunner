using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private Layout defaultLayout;

    public static SaveManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Layout GetLayout()
    {
        //No Save / Load supported now, sending default
        return defaultLayout;
    }
}
