using System.Collections.Generic;
using UnityEngine;

public class CardDataManager : MonoBehaviour
{
    public static CardDataManager Instance;

    [HideInInspector] public List<CardData> cards = new();
    [HideInInspector] public CardData CardData { get { return cards[currentIndex]; } set { cards[currentIndex] = value; } }
    public int currentIndex;
    private readonly Dictionary<CardAttrb, ICardAttributeField> attributeFields = new();
    private readonly Serializer serializer = new();

    private List<CardTab> tabs = new();
    [SerializeField] private GameObject tabPrefab;
    [SerializeField] private Transform tabParent;
    [SerializeField] private Vector2 tabOffset;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cards.Add(new CardData());
        SetupNewCardTab();
        currentIndex = 0;
    }

    public void SetCardAttribute(CardAttrb key, object value)
    {
        Debug.Log("attribute set: " + value.ToString());
        CardData.attributes[key] = value;
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
        serializer.Save(CardData);
    }

    public void LoadCardFromFile()
    {
        CardData = serializer.Load();
        CardPreview.Instance.SetCardFromData(CardData);
        SetAttributeFields(CardData);
    }

    public void NewCard()
    {
        CardData = new();
        CardPreview.Instance.SetCardFromData(CardData);
        SetAttributeFields(CardData);
    }

    public void SetActiveIndex(int index)
    {
        if (cards.Count < index) { return; }

        cards[currentIndex] = CardData;
        currentIndex = index;
        CardData = cards[index];
        CardPreview.Instance.SetCardFromData(CardData);
        SetAttributeFields(CardData);
    }

    private void SetupNewCardTab()
    {
        tabs.Add(Instantiate(tabPrefab, tabParent).GetComponent<CardTab>());
        tabs[^1].Setup(tabs.Count - 1, tabOffset);
        tabs[^1].SetTabName(CardData.attributes[CardAttrb.name].ToString());
    }

    private void SetAttributeFields(CardData cardData)
    {
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
