using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectPartAdder : MonoBehaviour
{
    private EffectEditor editor;
    private Toggle toggle;

    private void Start()
    {
        editor = FindObjectOfType<EffectEditor>();
        toggle = GetComponent<Toggle>();
    }

    public void ToggleEffectPart(bool value)
    {
        if (value)
        {
            editor.AddTrigger(name);
        }
        else
        {
            editor.RemoveTrigger(name);
        }
    }

    public void SetState(bool value)
    {
        if (value)
        {
            toggle.isOn = !toggle.isOn;
        }
    }
}
