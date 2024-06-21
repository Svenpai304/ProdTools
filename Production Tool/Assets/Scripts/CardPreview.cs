using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPreview : MonoBehaviour
{
    public static CardPreview Instance { get; private set; }
    [HideInInspector] public CardData cardData = new();
    private XMLSerializer serializer = new();

    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text attack, health, tier;
    [SerializeField] private ImageSelector image;
    [SerializeField] private TMP_Text effectDescription;
    [SerializeField] private EffectHolder effectHolder;
    [SerializeField] private Image rarityImage;

    [SerializeField] private Color[] rarityColors;

    private void Awake()
    {
        Instance = this;
    }

    public void SetCardFromData(CardData _cardData)
    {
        SetName(_cardData.cardName);
        SetAttack(_cardData.attack);
        SetHealth(_cardData.health);
        SetTier(_cardData.tier);
        SetImage(_cardData.imagePath);
        SetEffectDesc(_cardData.effectDescription);
        SetEffects(_cardData.effects);
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

    public void SetEffects(List<CardEffect> _effects)
    {
        effectHolder.SetAllEffects(_effects);
        cardData.effects = _effects;
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

    public void SetImageScale(Vector2 _scale)
    {
        cardData.imageScale = _scale;
    }

    public void SetImageOffset(Vector2 _offset)
    {
        cardData.imageOffset = _offset;
    }

    public void SaveCardToFile()
    {
        serializer.Save(cardData);
    }

    public void LoadCardFromFile()
    {
        SetCardFromData(serializer.Load());
    }


}
