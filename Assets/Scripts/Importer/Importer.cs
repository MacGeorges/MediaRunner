using B83.Win32;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public struct DropInfo
{
    public string file;
    public Vector2 pos;
}

public enum DataType
{
    None = 0,
    Image = 1,
    Video = 2,
    Audio = 3,
    NDI = 4
}

[RequireComponent(typeof(ImageImporter))]
[RequireComponent(typeof(VideoImporter))]
[RequireComponent(typeof(AudioImporter))]
public class Importer : MonoBehaviour
{
    [SerializeField]
    private SupportedFiles supportedFiles;

    public ImageImporter imageImporter { get; private set; }
    public VideoImporter videoImporter { get; private set; }
    public AudioImporter audioImporter { get; private set; }

    public static Importer Instance;

    private void Awake()
    {
        Instance = this;

        imageImporter = GetComponent<ImageImporter>();
        videoImporter = GetComponent<VideoImporter>();
        audioImporter = GetComponent<AudioImporter>();
    }

    void OnEnable()
    {
        UnityDragAndDropHook.InstallHook();
        UnityDragAndDropHook.OnDroppedFiles += OnFiles;

    }

    void OnDisable()
    {
        UnityDragAndDropHook.UninstallHook();
    }

    private void OnFiles(List<string> aFiles, POINT aPos)
    {
        foreach (var f in aFiles)
        {
            var fi = new System.IO.FileInfo(f);
            var ext = fi.Extension.ToLower();

            DropInfo dropInfo = new DropInfo
            {
                file = f,
                pos = new Vector2(aPos.x, aPos.y)
            };

            DataType importType = DataType.None;

            if (supportedFiles.supportedImageFiles.Contains(ext))
            {
                importType = DataType.Image;
            }

            if (supportedFiles.supportedVideoFiles.Contains(ext))
            {
                importType = DataType.Video;
            }

            if (supportedFiles.supportedAudioFiles.Contains(ext))
            {
                importType = DataType.Audio;
            }

            VignettesManager.Instance.HoveredVignette?.SetResource(importType, fi.FullName);
        }
    }
}