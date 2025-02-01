using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Layout", menuName = "Scriptable Objects/Layout")]
public class Layout : ScriptableObject
{
    public List<Row> Rows;
}
