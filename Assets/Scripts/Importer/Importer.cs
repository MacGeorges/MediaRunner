using B83.Win32;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public struct DropInfo
{
    public string file;
    public Vector2 pos;
}

[RequireComponent(typeof(ImageImporter))]
[RequireComponent(typeof(VideoImporter))]
[RequireComponent(typeof(AudioImporter))]
public class Importer : MonoBehaviour
{
    [SerializeField]
    private SupportedFiles supportedFiles;

    private ImageImporter imageImporter;
    private VideoImporter videoImporter;
    private AudioImporter audioImporter;

    private void Awake()
    {
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

            if (supportedFiles.supportedImageFiles.Contains(ext))
            {
                imageImporter.ImportImage(fi.FullName, dropInfo);
            }

            if (supportedFiles.supportedVideoFiles.Contains(ext))
            {
                videoImporter.ImportVideo(fi.FullName);
            }

            if (supportedFiles.supportedAudioFiles.Contains(ext))
            {
                audioImporter.ImportAudio(fi.FullName);
            }
        }
    }
}