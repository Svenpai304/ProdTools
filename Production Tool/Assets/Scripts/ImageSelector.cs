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
        if (target == null) { return; }
        string[] options = StandaloneFileBrowser.OpenFilePanel("Select image file", Application.persistentDataPath, new ExtensionFilter[1] { filter }, false);
        if (options.Length == 0) { return; }

        filename = options[0];
        StartCoroutine(LoadImage(filename));
    }
    public IEnumerator LoadImage(string url)
    {
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

    public void SetScaleX(string str)
    {
        float value;
        try { value = float.Parse(str); }
        catch { value = 1; }
        Debug.Log(value);
        target.uvRect = new Rect(target.uvRect.x, target.uvRect.y, value, target.uvRect.height);
        CardPreview.Instance.SetImageScale(new Vector2(value, target.uvRect.height));
    }

    public void SetScaleY(string str)
    {
        float value;
        try { value = float.Parse(str); }
        catch { value = 1; }
        target.uvRect = new Rect(target.uvRect.x, target.uvRect.y, target.uvRect.width, value);
        CardPreview.Instance.SetImageScale(new Vector2(target.uvRect.width, value));
    }

    public void SetOffsetX(string str)
    {
        float value;
        try { value = float.Parse(str); }
        catch { value = 0; }
        target.uvRect = new Rect(value, target.uvRect.y, target.uvRect.width, target.uvRect.height);
        CardPreview.Instance.SetImageOffset(new Vector2(value, target.uvRect.y));
    }

    public void SetOffsetY(string str)
    {
        float value;
        try { value = float.Parse(str); }
        catch { value = 0; }
        target.uvRect = new Rect(target.uvRect.x, value, target.uvRect.width, target.uvRect.height);
        CardPreview.Instance.SetImageOffset(new Vector2(target.uvRect.x, value));
    }
}
