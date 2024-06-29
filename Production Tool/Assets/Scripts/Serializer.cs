using SFB;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

public class Serializer
{

    ISerializationStrategy<CardData> serializer;

    public Serializer()
    {
        serializer = new BinaryStrategy<CardData>();
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
                File.Delete(url);
            }
        }
        CardPreview.Instance.SetCardFromData(data);
        return data;
    }
    public void Save(CardData data)
    {
        foreach (object attrb in data.attributes.Values)
        {
            Debug.Log(attrb.GetType() + ": " + attrb.ToString());
        }
        string url = StandaloneFileBrowser.SaveFilePanel("Save file", Application.persistentDataPath, "New card", "card");
        if (data != null)
        {
            try
            {
                FileStream fstream = new(url, FileMode.Create);
                serializer.Serialize(fstream, data);
                fstream.Close();
            }
            catch (System.Exception e)
            {
                Debug.Log("Serialization Error: " + e.Message);
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
        T data = (T)formatter.Deserialize(stream);
        return data;
    }
    public void Serialize(Stream stream, T data)
    {
        formatter.Serialize(stream, data);
    }
}

public class JsonStrategy<T> : ISerializationStrategy<T>
{
    public T Deserialize(Stream stream)
    {
        T data = JsonUtility.FromJson<T>(new StreamReader(stream).ReadToEnd());
        return data;
    }
    public void Serialize(Stream stream, T data)
    {
        var jsonData = JsonUtility.ToJson(data);
        Debug.Log(jsonData);
        new StreamWriter(stream).Write(jsonData);
    }
}

public class BinaryStrategy<T> : ISerializationStrategy<T>
{
    private BinaryFormatter formatter;
    public BinaryStrategy()
    {
        formatter = new BinaryFormatter();
    }
    public T Deserialize(Stream stream)
    {
        T data = (T)formatter.Deserialize(stream);
        return data;
    }
    public void Serialize(Stream stream, T data)
    {
        formatter.Serialize(stream, data);
    }
}

