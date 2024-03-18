using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEditor : MonoBehaviour
{
    private CardEffect currentEffect;

    private List<EffectPartAdder> effectPartAdders = new();

    private void Start()
    {
        effectPartAdders.AddRange(GetComponentsInChildren<EffectPartAdder>());
    }

    public void SetCurrentEffect(CardEffect effect)
    {
        currentEffect = effect;
        foreach(EffectPartAdder part in effectPartAdders)
        {
            part.SetState(currentEffect.triggers.Contains(part.name));
        }
    }

    public void SaveCurrentEffect()
    {

    }

    public void AddTrigger(string id)
    {
        if (!currentEffect.triggers.Contains(id))
        {
            currentEffect.triggers.Add(id);
        }
    }

    public void RemoveTrigger(string id)
    {
        if (currentEffect.triggers.Contains(id))
        {
            currentEffect.triggers.Remove(id);
        }
    }
}
