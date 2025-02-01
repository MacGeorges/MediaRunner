using System.Collections.Generic;
using UnityEngine;
using System;

public class StructsEnums{}

[Serializable]
public struct Row
{
    public List<VignetteData> vignettes;
}

[Serializable]
public struct VignetteData
{
    public DataType mode;
    public string dataPath;
    public float transitionSpeed;
}

public struct DropInfo
{
    public string file;
    public Vector2 pos;
}

[Serializable]
public enum DataType
{
    None = 0,
    Image = 1,
    Video = 2,
    Audio = 3,
    NDI = 4
}