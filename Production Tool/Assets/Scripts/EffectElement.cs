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
        nameText.text = NameFromEffect(effect, index);
    }

    public void SetIndex(int _index)
    {
        index = _index;
        nameText.text = NameFromEffect(effect, index);
    }

    public void Click()
    {
        holder.EditEffect(effect, index);
    }

    private string NameFromEffect(CardEffect effect, int index)
    {
        string trigger = string.Empty;
        string action = string.Empty;

        if(effect.triggers.Length > 0)
        {
            trigger = effect.triggers[0];
        }
        if (effect.actions.Length > 0)
        {
            action = effect.actions[0];
        }
        return $"{index}. {trigger}: {action}";
    }
}