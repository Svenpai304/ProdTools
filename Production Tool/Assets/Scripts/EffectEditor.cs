using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        if (effectPartAdders.Count > 0) { return; }
        effectPartAdders.AddRange(GetComponentsInChildren<EffectPartAdder>());
        Debug.Log(effectPartAdders.Count);
        holder = FindObjectOfType<EffectHolder>();
    }

    public void OpenEditor(CardEffect effect, int index)
    {
        UIParent.gameObject.SetActive(true);
        SetCurrentEffect(effect);
        currentIndex = index;
        Debug.Log("Editing effect at index " + currentIndex);
    }

    public void CloseEditor()
    {
        Debug.Log("Closing editor with " + currentEffect.triggers.Count + " triggers");
        holder.SetEffectElement(currentEffect, currentIndex);
        UIParent.gameObject.SetActive(false);
    }

    public void SetCurrentEffect(CardEffect effect)
    {
        Debug.Log("Setting effect settings");
        currentEffect = effect;
        currentEffect.triggers ??= new();
        foreach (EffectPartAdder part in effectPartAdders)
        {
            part.SetState(currentEffect.triggers.Contains(part.name));
        }
    }

    public void AddTrigger(string id)
    {
        Debug.Log("Adding trigger: " + id);
        if (!currentEffect.triggers.Contains(id))
        {
            currentEffect.triggers.Add(id);
        }
    }

    public void RemoveTrigger(string id)
    {
        Debug.Log("Removing trigger: " + id);
        if (currentEffect.triggers.Contains(id))
        {
            currentEffect.triggers.Remove(id);
        }
    }
}
