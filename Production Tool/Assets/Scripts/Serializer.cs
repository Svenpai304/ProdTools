using SFB;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class XMLSerializer
{

    ISerializationStrategy<CardData> serializer;

    public XMLSerializer()
    {
        serializer = new XmlStrategy<CardData>();
    }

    public CardData Load()
    {
        string url = StandaloneFileBrowser.OpenFilePanel("Load file", Application.persistentDataPath, "card", false)[0];
        CardData data = new();
        if (File.Exists(url))
        {
            try
            {
                FileStream fstream = new(url, FileMode.Open);
                data = serializer.Deserialize(fstream);
                fstream.Close();
            }
            catch (System.Exception e)
            {
                Debug.LogError("Serialization Error: " + e.Message);
            }
        }
        CardPreview.Instance.SetCardFromData(data);
        return data;
    }
    public void Save(CardData data)
    {
        string url = StandaloneFileBrowser.SaveFilePanel("Save file", Application.persistentDataPath, "New card", "card");
        if (data != null)
        {
            try
            {
                FileStream fstream = new(url, FileMode.CreateNew);
                serializer.Serialize(fstream, data);
                fstream.Close();
            }
            catch (System.Exception e)
            {
                Debug.LogError("Serialization Error: " + e.Message);
            }
        }
    }
}
public interface ISerializationStrategy<T>
{
    T Deserialize(Stream stream);
    void Serialize(Stream stream, T data);
}

public class XmlStrategy<T> : ISerializationStrategy<T>
{
    private XmlSerializer formatter;
    public XmlStrategy()
    {
        formatter = new XmlSerializer(typeof(T));
    }
    public T Deserialize(Stream stream)
    {
        T data = default;
        data = (T)formatter.Deserialize(stream);
        return data;
    }
    public void Serialize(Stream stream, T data)
    {
        formatter.Serialize(stream, data);
    }
}

