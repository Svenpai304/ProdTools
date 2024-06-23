using System.Collections.Generic;
using UnityEngine;

public class EffectEditor : MonoBehaviour
{
    [SerializeField] private Transform UIParent;
    private EffectHolder holder;
    private CardEffect currentEffect;
    private int currentIndex;

    private List<EffectPartAdder> effectPartAdders = new();

    private List<string> currentTriggers = new();
    private List<string> currentTargets = new();
    private List<string> currentEffects = new();
    private List<string> currentEndActions = new();


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
        currentEffect.triggers = currentTriggers.ToArray();
        currentEffect.targets = currentTargets.ToArray();
        currentEffect.effects = currentEffects.ToArray();
        currentEffect.endActions = currentEndActions.ToArray();
        Debug.Log("Closing editor with " + currentEffect.triggers.Length + " triggers");
        holder.SetEffectElement(currentEffect, currentIndex);
        UIParent.gameObject.SetActive(false);
    }

    public void SetCurrentEffect(CardEffect effect)
    {
        Debug.Log("Setting effect settings");
        currentEffect = effect;
        currentEffect.triggers ??= new string[0];
        currentEffect.targets ??= new string[0];
        currentEffect.effects ??= new string[0];
        currentEffect.endActions ??= new string[0];
        currentTriggers.Clear();
        currentTargets.Clear();
        currentEffects.Clear();
        currentEndActions.Clear();
        if (currentEffect.triggers.Length > 0)
        {
            currentTriggers.AddRange(currentEffect.triggers);
        }
        if (currentEffect.targets.Length > 0)
        {
            currentTargets.AddRange(currentEffect.targets);
        }
        if (currentEffect.effects.Length > 0) 
        { 
            currentEffects.AddRange(currentEffect.effects); 
        }
        if (currentEffect.endActions.Length > 0)
        {
            currentEndActions.AddRange(currentEffect.endActions);
        }
        foreach (EffectPartAdder part in effectPartAdders)
        {
            part.SetState(currentTriggers.Contains(part.name) || currentTargets.Contains(part.name) || currentEffects.Contains(part.name) || currentEndActions.Contains(part.name));
        }
    }

    public void AddTrigger(string id)
    {
        Debug.Log("Adding trigger: " + id);
        if (!currentTriggers.Contains(id))
        {
            currentTriggers.Add(id);
        }
    }

    public void RemoveTrigger(string id)
    {
        Debug.Log("Removing trigger: " + id);
        if (currentTriggers.Contains(id))
        {
            currentTriggers.Remove(id);
        }
    }
}
