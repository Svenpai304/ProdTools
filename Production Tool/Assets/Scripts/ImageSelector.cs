using SFB;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageSelector : MonoBehaviour, ICardAttributeField
{
    public RawImage target;
    public TMP_Text text;
    public TMP_Text textPlaceholder;
    public TMP_InputField offsetX, offsetY, scaleX, scaleY;
    public string[] fileExtensions;
    private ExtensionFilter filter = new();
    public string filename;

    private bool loading = false;

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
        CardDataManager.Instance.SetAttributeField(CardAttrb.imagePath, this);
        CardDataManager.Instance.SetAttributeField(CardAttrb.imageOffset, this);
        CardDataManager.Instance.SetAttributeField(CardAttrb.imageScale, this);
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
        if (loading || url == null) { yield break; }
        if(url == "Invalid path") { HandleInvalidPath(); yield break; }

        loading = true;
        SetText(url);
        Debug.Log("Loading image: " + url);
        var finalPath = Path.Combine("file://", url);
        UnityWebRequest file = UnityWebRequestTexture.GetTexture(finalPath);
        yield return file.SendWebRequest();
        loading = false;
        if (file.result != UnityWebRequest.Result.Success) 
        {
            HandleInvalidPath(); yield break; 
        }

        Texture2D texture = (((DownloadHandlerTexture)file.downloadHandler).texture);
        target.texture = texture;
        CardDataManager.Instance.SetCardAttribute(CardAttrb.imagePath, url);
    }

    private void SetText(string name)
    {
        textPlaceholder.text = "";
        text.text = name;
    }

    public void SetScaleX(string str)
    {
        float value;
        try { value = float.Parse(str); }
        catch { value = 1; }
        target.uvRect = new Rect(target.uvRect.x, target.uvRect.y, value, target.uvRect.height);
        CardDataManager.Instance.SetCardAttribute(CardAttrb.imageScale, new float[] { target.uvRect.size.x, target.uvRect.size.y });
    }

    public void SetScaleY(string str)
    {
        float value;
        try { value = float.Parse(str); }
        catch { value = 1; }
        target.uvRect = new Rect(target.uvRect.x, target.uvRect.y, target.uvRect.width, value);
        CardDataManager.Instance.SetCardAttribute(CardAttrb.imageScale, new float[] { target.uvRect.size.x, target.uvRect.size.y });
    }

    public void SetOffsetX(string str)
    {
        float value;
        try { value = float.Parse(str); }
        catch { value = 0; }
        target.uvRect = new Rect(value, target.uvRect.y, target.uvRect.width, target.uvRect.height);
        CardDataManager.Instance.SetCardAttribute(CardAttrb.imageOffset, new float[] { target.uvRect.position.x, target.uvRect.position.y });
    }

    public void SetOffsetY(string str)
    {
        float value;
        try { value = float.Parse(str); }
        catch { value = 0; }
        target.uvRect = new Rect(target.uvRect.x, value, target.uvRect.width, target.uvRect.height);
        CardDataManager.Instance.SetCardAttribute(CardAttrb.imageOffset, new float[] { target.uvRect.position.x, target.uvRect.position.y });
    }

    public void SetFieldValue(CardAttrb attrb, object value)
    {
        switch (attrb)
        {
            case CardAttrb.imagePath:
                if (value == default || value == null || ((string)value).Length < 1)
                {
                    HandleInvalidPath();
                    break; 
                }
                StartCoroutine(LoadImage((string)value)); break;
            case CardAttrb.imageOffset:
                if (value == default) { value = new float[] { 0, 0 }; }
                SetOffsetX(((float[])value)[0].ToString());
                SetOffsetY(((float[])value)[1].ToString());
                offsetX.text = ((float[])value)[0].ToString();
                offsetY.text = ((float[])value)[1].ToString(); break;
            case CardAttrb.imageScale:
                if (value == default) { value = new float[] { 1, 1 }; }
                SetScaleX(((float[])value)[0].ToString());
                SetScaleY(((float[])value)[1].ToString());
                scaleX.text = ((float[])value)[0].ToString();
                scaleY.text = ((float[])value)[1].ToString(); break;
        }
    }

    private void HandleInvalidPath()
    {
        target.texture = null;
        SetText("Invalid path");
        CardDataManager.Instance.SetCardAttribute(CardAttrb.imagePath, "Invalid path");
        Debug.Log("Failed to load image: Invalid path");
    }
}
