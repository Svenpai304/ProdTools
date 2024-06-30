using UnityEngine;
using UnityEngine.UI;

public class EffectPartAdder : MonoBehaviour
{
    private EffectEditor editor;
    private Toggle toggle;
    private bool setup;

    private void Awake()
    {
        editor = FindObjectOfType<EffectEditor>();
        toggle = GetComponent<Toggle>();
    }

    public void ToggleTrigger(bool value)
    {
        if (setup) { setup = false; return; }

        if (value)
        {
            editor.AddTrigger(name);
        }
        else
        {
            editor.RemoveTrigger(name);
        }
    }

    public void ToggleAction(bool value)
    {
        if (setup) { setup = false; return; }

        if (value)
        {
            editor.AddAction(name);
        }
        else
        {
            editor.RemoveAction(name);
        }
    }

    public void ToggleTarget(bool value)
    {
        if (setup) { setup = false; return; }

        if (value)
        {
            editor.AddTarget(name);
        }
        else
        {
            editor.RemoveTarget(name);
        }
    }
    public void ToggleEndAction(bool value)
    {
        if (setup) { setup = false; return; }

        if (value)
        {
            editor.AddEndAction(name);
        }
        else
        {
            editor.RemoveEndAction(name);
        }
    }

    public void SetState(bool value)
    {
        Debug.Log("Setting toggle state");
        setup = false;
        toggle.isOn = value;
    }
}
