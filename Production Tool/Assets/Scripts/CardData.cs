using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardData
{
    public string cardName;
    public List<string> tags = new();
    public int attack, health, tier;

    public string imagePath;
    public Vector2 imageScale, imageOffset;
    public int rarity;
    public int theme; 

    public string effectDescription;
    public List<CardEffect> effects;

}
