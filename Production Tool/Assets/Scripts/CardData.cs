using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
