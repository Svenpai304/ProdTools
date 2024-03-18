using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHolder : MonoBehaviour
{
    [SerializeField] private float elementSpacing;
    [SerializeField] private GameObject effectEditorUI;
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private List<CardEffect> effects = new();

    
    public void SetAllEffects(List<CardEffect> _effects)
    {
        effects = _effects;
    }

    public void AddEffect(CardEffect effect)
    {

    }
}
