using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardData
{
    [SerializeField]public Dictionary<CardAttrb, object> attributes = new();

    public static T GetAttribute<T>(CardAttrb attrb, CardData data)
    {
        if (data.attributes.ContainsKey(attrb))
        {
            return (T)data.attributes[attrb];
        }
        else
        {
            return default;
        }
    }
}

public enum CardAttrb { 
    name, 
    tags, 
    attack,  
    health, 
    tier, 
    imagePath, 
    imageScale, 
    imageOffset, 
    rarity, 
    cardClass,
    theme,
    effectDesc, 
    effects 
};
