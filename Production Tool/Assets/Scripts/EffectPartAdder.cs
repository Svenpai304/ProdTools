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

    public void ToggleEffectPart(bool value)
    {
        Debug.Log("Toggling effect");
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

    public void SetState(bool value)
    {
        Debug.Log("Setting toggle state");
        setup = false;
        toggle.isOn = value;
    }
}
