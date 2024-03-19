using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEditor : MonoBehaviour
{
    [SerializeField] private Transform UIParent;
    private EffectHolder holder;
    private CardEffect currentEffect;
    private int currentIndex;

    private List<EffectPartAdder> effectPartAdders = new();

    private void OnEnable()
    {
        if(effectPartAdders.Count > 0) { return; }
        effectPartAdders.AddRange(GetComponentsInChildren<EffectPartAdder>());
        Debug.Log(effectPartAdders.Count);
        holder = FindObjectOfType<EffectHolder>();
    }

    public void OpenEditor(CardEffect effect, int index)
    {
        SetCurrentEffect(effect);
        currentIndex = index;
        UIParent.gameObject.SetActive(true);
    }

    public void CloseEditor()
    {
        holder.SetEffectElement(currentEffect, currentIndex);
        UIParent.gameObject.SetActive(false);
    }

    public void SetCurrentEffect(CardEffect effect)
    {
        if(currentEffect == effect) { return; }
        currentEffect = effect;
        foreach(EffectPartAdder part in effectPartAdders)
        {
            part.SetState(currentEffect.triggers.Contains(part.name));
        }
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
