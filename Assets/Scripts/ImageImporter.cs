using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using B83.Win32;
using UnityEngine.Video;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ImageImporter : MonoBehaviour
{
    public Vignette currentVignette;

    [SerializeField]
    private SupportedFiles supportedFiles;

    struct DropInfo
    {
        public string file;
        public Vector2 pos;
    }

    void OnEnable ()
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

            if (supportedFiles.supportedFiles.Contains(ext))
            {
                DropInfo dropInfo = new DropInfo
                {
                    file = f,
                    pos = new Vector2(aPos.x, aPos.y)
                };

                if (currentVignette)
                {
                    currentVignette.SetSprite(LoadSprite(dropInfo));
                }

                break;
            }
        }
    }

    private Sprite LoadSprite(DropInfo dropInfo)
    {
        if (dropInfo.file == null)
            return null;

        var data = System.IO.File.ReadAllBytes(dropInfo.file);
        var tex = new Texture2D(1, 1);
        tex.LoadImage(data);

        return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}
