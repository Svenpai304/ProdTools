using SFB;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageSelector : MonoBehaviour
{
    public RawImage target;
    public TMP_Text text;
    public TMP_Text textPlaceholder;
    public string[] fileExtensions;
    private ExtensionFilter filter = new();
    public string filename;

    private void Start()
    {
        if (fileExtensions != null)
        {
            filter.Extensions = fileExtensions;
        }
        else
        {
            filter.Extensions = new string[1] { "png" };
        }
    }

    public void SelectFile()
    {
        StartCoroutine(SelectFileCoroutine());
    }
    public IEnumerator SelectFileCoroutine()
    {
        if (target == null) { yield break; }
        string[] options = StandaloneFileBrowser.OpenFilePanel("Select image file", Application.persistentDataPath, new ExtensionFilter[1] { filter }, false);
        if (options.Length == 0) { yield break; }

        filename = options[0];
        SetText();
        UnityWebRequest file = UnityWebRequestTexture.GetTexture("file://" + filename);
        yield return file.SendWebRequest();
        if (file.result != UnityWebRequest.Result.Success) { Debug.Log("Failed to load image"); yield break; }

        Texture2D texture = (((DownloadHandlerTexture)file.downloadHandler).texture);
        target.texture = texture;
    }

    private void SetText()
    {
        textPlaceholder.text = "";
        text.text = filename;
    }
}
