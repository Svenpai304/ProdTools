using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class IntSetter : MonoBehaviour, ICardAttributeField
{
    private int value;
    [SerializeField] private TMP_InputField text;
    [SerializeField] private TMP_Dropdown dropdown;
    public CardAttrb attributeName = CardAttrb.attack;
    public UnityEvent<int> OnValueChanged;

    private void Start()
    {
        CardDataManager.Instance.SetAttributeField(attributeName, this);
    }

    public void SetInt(int value)
    {
        CardDataManager.Instance.SetCardAttribute(attributeName, value);
        OnValueChanged?.Invoke(value);
    }

    public void SetIntFromString(string str)
    {
        try { value = int.Parse(str); }
        catch { value = 1; }
        SetInt(value);
    }

    public void SetFieldValue(CardAttrb attrb, object value)
    {
        try
        {
            if (text != null)
            {
                text.text = value.ToString();
            }
        }
        catch { }
        try
        {
            if (dropdown != null)
            {
                dropdown.value = (int)value;
            }
        }
        catch { }
    }
}
