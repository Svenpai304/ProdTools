using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHolder : MonoBehaviour
{
    [SerializeField] private float elementSpacing;
    [SerializeField] private EffectEditor editor;
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private Transform effectParent;
    [SerializeField] private List<EffectElement> elements= new();
    private int lastIndex;

    public void SetAllEffects(List<CardEffect> _effects)
    {
        if(elements.Count > 0)
        {
            foreach(EffectElement element in elements)
            {
                Destroy(element.gameObject);
            }
        }
        for(int i = 0; i < _effects.Count; i++)
        {
            SetEffectElement(_effects[i], i);
        }
    }

    public void EditEffect(CardEffect effect, int index)
    {
        editor.OpenEditor(effect, index);
    }

    public void AddEffect()
    {
        editor.OpenEditor(new CardEffect(), elements.Count);
        lastIndex = elements.Count;
    }

    public void RemoveEffect()
    {
        Destroy(elements[lastIndex]);
        elements.RemoveAt(lastIndex);
        lastIndex--;
    }

    public void SetEffectElement(CardEffect effect, int index)
    {
        if(index < elements.Count)
        {
            elements[index].Setup(effect, index, this);
        }
        else
        {
            EffectElement newElement = Instantiate(effectPrefab, effectParent).GetComponent<EffectElement>();
            newElement.transform.Translate(new Vector2(0, elementSpacing * index));
            newElement.Setup(effect, index, this);
            elements.Add(newElement);
        }
    }
}
