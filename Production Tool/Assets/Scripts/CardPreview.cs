using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Burst;
using UnityEngine;
using UnityEngine.UI;

public class CardPreview : MonoBehaviour
{
    private CardData cardData = new();

    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text attack, health, tier;
    [SerializeField] private ImageSelector image;
    [SerializeField] private TMP_Text effectDescription;
    [SerializeField] private Image rarityImage;

    [SerializeField] private Color[] rarityColors;

    public void SetCardFromData(CardData _cardData)
    {
        SetName(_cardData.cardName);
        SetAttack(_cardData.attack);
        SetHealth(_cardData.health);
        SetTier(_cardData.tier);
        SetImage(_cardData.imagePath);
        SetEffectDesc(_cardData.effectDescription);
        SetRarity(_cardData.rarity);
        SetTheme(_cardData.theme);
    }

    public void SetName(string _name)
    {
        cardName.SetText(_name);
        cardData.cardName = _name;
    }

    public void SetTags(string _tags)
    {
        string[] separatedTags = _tags.Split(',');
        cardData.tags = separatedTags.ToList();
    }

    public void SetAttack(int _attack)
    {
        attack.SetText(_attack.ToString());
        cardData.attack = _attack;
    }

    public void SetHealth(int _health)
    {
        health.SetText(_health.ToString());
        cardData.health = _health;
    }

    public void SetTier(int _tier)
    {
        tier.SetText(_tier.ToString());
        cardData.tier = _tier;
    }

    public void SetImage(string _imagePath)
    {
        image.StartCoroutine(image.LoadImage(_imagePath));
        cardData.imagePath = _imagePath;
    }

    public void SetEffectDesc(string _effectDesc)
    {
        effectDescription.SetText(_effectDesc.ToString());
        cardData.effectDescription = _effectDesc;
    }

    public void SetRarity(int _rarity)
    {
        if (_rarity >= rarityColors.Length) { _rarity = rarityColors.Length - 1; }
        rarityImage.color = rarityColors[_rarity];
        cardData.rarity = _rarity;
    }

    public void SetTheme(int _theme)
    {
        cardData.theme = _theme;
    }


}
