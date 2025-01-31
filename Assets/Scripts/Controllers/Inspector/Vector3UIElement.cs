using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
using static UnityEngine.Rendering.DebugUI;

public class Vector3UIElement : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField x;
    [SerializeField]
    private TMP_InputField y;
    [SerializeField]
    private TMP_InputField z;

    private float xValue;
    private float yValue;
    private float zValue;

    public UnityEvent<Vector3> OnValueUpdated;

    public void Init(Vector3 InitValues)
    {
        OnXUpdated(InitValues.x.ToString());
        OnYUpdated(InitValues.y.ToString());
        OnZUpdated(InitValues.z.ToString());
    }

    public void OnXUpdated(string newXValue)
    {
        if (!newXValue.Contains("."))
        {
            x.text = newXValue + ".0";
        }

        if (newXValue.StartsWith("."))
        {
            x.text = "0" + newXValue;
        }

        xValue = float.Parse(newXValue);
        OnFieldUpdated();
    }

    public void OnYUpdated(string newYValue)
    {
        if (!newYValue.Contains("."))
        {
            y.text = newYValue + ".0";
        }

        if (newYValue.StartsWith("."))
        {
            y.text = "0" + newYValue;
        }

        if (!newYValue.Contains(".")) { newYValue = newYValue + ".0"; }
        yValue = float.Parse(newYValue);
        OnFieldUpdated();
    }

    public void OnZUpdated(string newZValue)
    {
        if (!newZValue.Contains("."))
        {
            z.text = newZValue + ".0";
        }

        if (newZValue.StartsWith("."))
        {
            z.text = "0" + newZValue;
        }

        if (!newZValue.Contains(".")) { newZValue = newZValue + ".0"; }
        zValue = float.Parse(newZValue);
        OnFieldUpdated();
    }

    private void OnFieldUpdated()
    {
        OnValueUpdated.Invoke(new Vector3(xValue, yValue, zValue));
    }
}
