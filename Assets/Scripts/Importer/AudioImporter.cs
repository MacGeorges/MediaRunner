using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AudioImporter : MonoBehaviour
{
    public void ImportAudio(string path, Action<AudioClip> callback)
    {
        StartCoroutine(GetAudioClip(path, callback));
    }

    IEnumerator GetAudioClip(string path, Action<AudioClip> callback)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.OGGVORBIS))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                callback.Invoke(DownloadHandlerAudioClip.GetContent(www));
            }
        }
    }
}
