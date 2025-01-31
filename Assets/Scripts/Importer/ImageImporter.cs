using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using B83.Win32;
using UnityEngine.Video;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class ImageImporter : MonoBehaviour
{
    public void ImportImage(string path, DropInfo dropInfo)
    {
        VignettesManager.instance.currentVignette?.SetSprite(LoadSprite(dropInfo));
    }

    public Sprite LoadSprite(DropInfo dropInfo)
    {
        if (dropInfo.file == null)
            return null;

        var data = System.IO.File.ReadAllBytes(dropInfo.file);
        var tex = new Texture2D(1, 1);
        tex.LoadImage(data);

        return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}
