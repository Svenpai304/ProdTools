using System.Collections.Generic;
using UnityEngine;

public class EffectEditor : MonoBehaviour
{
    [SerializeField] private Transform UIParent;
    private EffectHolder holder;
    private CardEffect currentEffect;
    private int currentIndex;

    private List<EffectPartAdder> effectPartAdders = new();
    private List<EffectValueSetter> effectValueSetters = new();

    private List<string> currentTriggers = new();
    private List<string> currentTargets = new();
    private List<string> currentActions = new();
    private List<string> currentEndActions = new();


    private void OnEnable()
    {
        if (effectPartAdders.Count > 0) { return; }
        effectPartAdders.AddRange(GetComponentsInChildren<EffectPartAdder>());
        effectValueSetters.AddRange(GetComponentsInChildren<EffectValueSetter>());
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
        currentEffect.actions = currentActions.ToArray();
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
        currentEffect.actions ??= new string[0];
        currentEffect.endActions ??= new string[0];
        currentTriggers.Clear();
        currentTargets.Clear();
        currentActions.Clear();
        currentEndActions.Clear();
        if (currentEffect.triggers.Length > 0)
        {
            currentTriggers.AddRange(currentEffect.triggers);
        }
        if (currentEffect.targets.Length > 0)
        {
            currentTargets.AddRange(currentEffect.targets);
        }
        if (currentEffect.actions.Length > 0) 
        { 
            currentActions.AddRange(currentEffect.actions); 
        }
        if (currentEffect.endActions.Length > 0)
        {
            currentEndActions.AddRange(currentEffect.endActions);
        }
        foreach (EffectPartAdder part in effectPartAdders)
        {
            part.SetState(currentTriggers.Contains(part.name) || currentTargets.Contains(part.name) || currentActions.Contains(part.name) || currentEndActions.Contains(part.name));
        }
        foreach(EffectValueSetter value in effectValueSetters)
        {
            switch(value.valueType)
            {
                case EffectValueSetter.EffectValueType.NumericAction: value.numericValue.text = currentEffect.actionNumericalValue.ToString(); break;
                case EffectValueSetter.EffectValueType.NumericTarget: value.numericValue.text = currentEffect.targetNumericalValue.ToString(); break;
                case EffectValueSetter.EffectValueType.TextAction: value.textValue.text = currentEffect.actionTextValue; break;
                case EffectValueSetter.EffectValueType.TextTarget: value.textValue.text = currentEffect.targetTextValue; break;
            }
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

    public void AddAction(string id)
    {
        if (!currentActions.Contains(id))
        {
            currentActions.Add(id);
        }
    }
    public void RemoveAction(string id)
    {
        if (currentActions.Contains(id))
        {
            currentActions.Remove(id);
        }
    }

    public void AddTarget(string id)
    {
        if (!currentTargets.Contains(id))
        {
            currentTargets.Add(id);
        }
    }
    public void RemoveTarget(string id)
    {
        if (currentTargets.Contains(id))
        {
            currentTargets.Remove(id);
        }
    }

    public void AddEndAction(string id)
    {
        if (!currentActions.Contains(id))
        {
            currentActions.Add(id);
        }
    }
    public void RemoveEndAction(string id)
    {
        if (currentActions.Contains(id))
        {
            currentActions.Remove(id);
        }
    }

    public void SetActionNumericalValue(float value)
    {
        currentEffect.actionNumericalValue = value;
    }

    public void SetActionTextValue(string value)
    {
        currentEffect.actionTextValue = value;
    }

    public void SetTargetNumericalValue(float value)
    {
        currentEffect.targetNumericalValue = value;
    }

    public void SetTargetTextValue(string value)
    {
        currentEffect.targetTextValue = value;
    }
}
