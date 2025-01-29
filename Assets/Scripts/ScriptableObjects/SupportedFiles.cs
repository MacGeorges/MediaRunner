using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SupportedFiles", menuName = "Scriptable Objects/SupportedFiles")]
public class SupportedFiles : ScriptableObject
{
    public List<string> supportedFiles;
}
