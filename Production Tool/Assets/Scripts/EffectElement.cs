using TMPro;
using UnityEngine;

public class EffectElement : MonoBehaviour
{
    [SerializeField] private EffectHolder holder;
    [SerializeField] public CardEffect effect = new();
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private int index;

    public void Setup(CardEffect _effect, int _index, EffectHolder _holder)
    {
        effect = _effect;
        index = _index;
        holder = _holder;
        nameText.text = $"{index}: {_effect.name}";
    }

    public void SetIndex(int _index)
    {
        index = _index;
        nameText.text = $"{index}: {effect.name}";
    }

    public void Click()
    {
        holder.EditEffect(effect, index);
    }
}