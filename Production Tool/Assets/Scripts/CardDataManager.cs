using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataManager : MonoBehaviour
{
    public static CardDataManager Instance;

    [HideInInspector] public CardData cardData = new();
    private readonly Dictionary<CardAttrb, ICardAttributeField> attributeFields = new();
    private readonly Serializer serializer = new();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void SetCardAttribute(CardAttrb key, object value)
    {
        Debug.Log("attribute set: " + value.ToString());
        cardData.attributes[key] = value;
    }

    public void SetAttributeField(CardAttrb attrb, ICardAttributeField field)
    {
        if (attributeFields.ContainsKey(attrb))
        {
            attributeFields[attrb] = field;
        }
        else
        {
            attributeFields.Add(attrb, field);
        }
    }

    public void SaveCardToFile()
    {
        serializer.Save(cardData);
    }

    public void LoadCardFromFile()
    {
        cardData = serializer.Load();
        CardPreview.Instance.SetCardFromData(cardData);
        foreach(CardAttrb attrb in attributeFields.Keys)
        {
            object value;
            if (cardData.attributes.ContainsKey(attrb))
            {
                value = cardData.attributes[attrb];
            }
            else { value = default; }
            attributeFields[attrb].SetFieldValue(attrb, value);
        }
    }

    public void NewCard()
    {
        cardData = new();
        CardPreview.Instance.SetCardFromData(cardData);
        foreach (CardAttrb attrb in attributeFields.Keys)
        {
            object value;
            if (cardData.attributes.ContainsKey(attrb))
            {
                value = cardData.attributes[attrb];
            }
            else { value = default; }
            attributeFields[attrb].SetFieldValue(attrb, value);
        }
    }
}
