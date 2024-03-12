using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst;
using UnityEngine;
using UnityEngine.UI;

public class CardPreview : MonoBehaviour
{
    private CardData cardData = new();

    [SerializeField]private TMP_Text cardName;
    [SerializeField] private TMP_Text attack, health, tier;
    [SerializeField] private ImageSelector image;
    [SerializeField] private TMP_Text effectDescription;

    public void SetCardFromData(CardData _cardData)
    {
        cardData = _cardData;
        cardName.SetText(_cardData.cardName);
        attack.SetText(_cardData.attack.ToString());
        health.SetText(_cardData.health.ToString());
        tier.SetText(_cardData.tier.ToString());
        image.StartCoroutine(image.LoadImage(_cardData.imagePath));
        effectDescription.SetText(_cardData.effectDescription);
    }

    public void SetName(string _name)
    {
        cardName.SetText(_name);
        cardData.cardName = _name;
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

    
}
