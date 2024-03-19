using TMPro;
using UnityEngine;

public class EffectElement : MonoBehaviour
{
    [SerializeField] private EffectHolder holder;
    [SerializeField] private CardEffect effect;
    [SerializeField] private TMP_Text nameText;
    private int index;

    public void Setup(CardEffect _effect, int _index, EffectHolder _holder)
    {
        effect = _effect;
        nameText.text = $"{index}: {_effect.name}";
        index = _index;
        holder = _holder;
    }

    public void Click()
    {
        holder.EditEffect(effect, index);
    }
}