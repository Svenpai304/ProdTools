using System.Collections.Generic;
using UnityEngine;

public class CardDataManager : MonoBehaviour
{
    public static CardDataManager Instance;

    [HideInInspector] public List<CardData> cards = new();
    [HideInInspector] public CardData CardData { get { return cards[currentIndex]; } set { cards[currentIndex] = value; } }
    public int currentIndex = 0;
    private readonly Dictionary<CardAttrb, ICardAttributeField> attributeFields = new();
    private readonly Serializer serializer = new();

    private readonly List<CardTab> tabs = new();
    [SerializeField] private GameObject tabPrefab;
    [SerializeField] private Transform tabParent;
    [SerializeField] private Vector2 tabOffset;
    [SerializeField] private int maxTabs;

    void Awake()
    {
        Instance = this;
        cards.Add(new CardData());
        SetupNewCardTab();
    }

    public void SetCardAttribute(CardAttrb key, object value)
    {
        if (CardData.attributes.TryGetValue(key, out object attr) && attr == value)
        {
            return;
        }
        //Debug.Log("attribute set: " + value.ToString());
        CardData.attributes[key] = value;
        tabs[currentIndex].SetUnsavedFlag(true);
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
    private void SetAttributeFieldValues(CardData cardData)
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

    public void SaveCardToFile()
    {
        try { tabs[currentIndex].SetTabName(CardData.attributes[CardAttrb.name].ToString()); }
        catch { Debug.Log("No valid name in card data"); }
        serializer.Save(CardData);
        tabs[currentIndex].SetUnsavedFlag(false);
    }

    public void LoadCardFromFile()
    {
        if(cards.Count >= maxTabs) { return; }
        CardData data = serializer.Load();
        if (data != null)
        {
            cards.Add(data);
            SetupNewCardTab();

            CardPreview.Instance.SetCardFromData(CardData);
            SetAttributeFieldValues(CardData);
            tabs[currentIndex].SetUnsavedFlag(false);
        }
    }

    public void NewCard()
    {
        if (cards.Count >= maxTabs) { return; }
        cards.Add(new CardData());
        SetupNewCardTab();
        CardPreview.Instance.SetCardFromData(CardData);
        SetAttributeFieldValues(CardData);
    }

    private void SetupNewCardTab()
    {
        tabs.Add(Instantiate(tabPrefab, tabParent).GetComponent<CardTab>());
        SetActiveIndex(tabs.Count - 1);
        tabs[^1].Setup(currentIndex, tabOffset * currentIndex);
        if (CardData.attributes.ContainsKey(CardAttrb.name))
        {
            tabs[^1].SetTabName(CardData.attributes[CardAttrb.name].ToString());
        }
        else
        {
            tabs[^1].SetTabName("New card");
        }
    }

    public void SetActiveIndex(int index)
    {
        if (cards.Count <= index || currentIndex == index) { return; }

        bool previousUnsaved = tabs[currentIndex].IsUnsaved;
        int previousIndex = currentIndex;
        cards[currentIndex] = CardData;
        Debug.Log(currentIndex);
        currentIndex = index;
        CardData = cards[index];
        tabs[currentIndex].SetTabActive(true);
        bool unsaved = tabs[currentIndex].IsUnsaved;
        CardPreview.Instance.SetCardFromData(CardData);
        SetAttributeFieldValues(CardData);
        tabs[currentIndex].SetUnsavedFlag(unsaved);
        tabs[previousIndex].SetTabActive(false);
        tabs[previousIndex].SetUnsavedFlag(previousUnsaved);
    }
}
