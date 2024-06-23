using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StringSetter : MonoBehaviour, ICardAttributeField
{
    [SerializeField] private TMP_InputField text;
    public CardAttrb attributeName = CardAttrb.attack;
    public UnityEvent<string> OnValueChanged;

    private void Start()
    {
        CardDataManager.Instance.SetAttributeField(attributeName, this);
    }
    public void SetString(string str)
    {
        CardDataManager.Instance.SetCardAttribute(attributeName, str);
        OnValueChanged?.Invoke(str);
    }

    public void SetSeparatedString(string str)
    {
        str.Replace(" ", "");
        string[] strings = str.Split(',');
        CardDataManager.Instance.SetCardAttribute(attributeName, strings);
    }

    public void SetFieldValue(CardAttrb attrb, object value)
    {
        if(value == null) { value = ""; }
        if (value.GetType() == typeof(string))
        {
            text.text = value.ToString();
        }
        if(value.GetType() == typeof(string[]))
        {
            string result = "";
            string[] strings = (string[])value;
            foreach (string s in strings)
            {
                result += s + ", ";
            }
            text.text = result[..^2];
        }
    }
}
