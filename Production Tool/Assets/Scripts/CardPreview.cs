using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardPreview : MonoBehaviour
{
    public static CardPreview Instance { get; private set; }

    [SerializeField] private TMP_Text cardName;
    [SerializeField] private TMP_Text attack, health, tier;
    [SerializeField] private ImageSelector image;
    [SerializeField] private TMP_Text effectDescription;
    [SerializeField] private Image rarityImage;

    [SerializeField] private Color[] rarityColors;

    private void Awake()
    {
        Instance = this;
    }

    public void SetCardFromData(CardData _cardData)
    {
        SetName(CardData.GetAttribute<string>(CardAttrb.name, _cardData));
        SetAttack(CardData.GetAttribute<int>(CardAttrb.attack, _cardData));
        SetHealth(CardData.GetAttribute<int>(CardAttrb.health, _cardData));
        SetTier(CardData.GetAttribute<int>(CardAttrb.tier, _cardData));
        SetImage(CardData.GetAttribute<string>(CardAttrb.imagePath, _cardData));
        SetEffectDesc(CardData.GetAttribute<string>(CardAttrb.effectDesc, _cardData));
        SetRarity(CardData.GetAttribute<int>(CardAttrb.rarity, _cardData));
        SetTheme(CardData.GetAttribute<int>(CardAttrb.theme, _cardData));
    }

    public void SetName(string _name)
    {
        cardName.SetText(_name);
    }

    public void SetAttack(int _attack)
    {
        attack.SetText(_attack.ToString());
    }

    public void SetHealth(int _health)
    {
        health.SetText(_health.ToString());
    }

    public void SetTier(int _tier)
    {
        tier.SetText(_tier.ToString());
    }

    public void SetImage(string _imagePath)
    {
        image.StartCoroutine(image.LoadImage(_imagePath));
    }

    public void SetEffectDesc(string _effectDesc)
    {
        _effectDesc ??= string.Empty;
        effectDescription.SetText(_effectDesc.ToString());
    }

    public void SetRarity(int _rarity)
    {
        if (_rarity >= rarityColors.Length) { _rarity = rarityColors.Length - 1; }
        rarityImage.color = rarityColors[_rarity];
    }

    public void SetTheme(int _theme)
    {
        //yes this definitely does something! i absolutely did not forget to implement it!!
    }
}
