using TMPro;
using UnityEngine;

public class EffectValueSetter : MonoBehaviour
{
    private EffectEditor editor;
    public TMP_InputField textValue;
    public TMP_InputField numericValue;
    public enum EffectValueType { NumericAction, NumericTarget, TextAction,  TextTarget }
    public EffectValueType valueType;

    private void Awake()
    {
        editor = FindObjectOfType<EffectEditor>();
    }

    public void SetNumericAction(string str)
    {
        int value;
        try { value = int.Parse(str); }
        catch { value = 1; }
        editor.SetActionNumericalValue(value);
    }

    public void SetTextAction(string str)
    {
        editor.SetActionTextValue(str);
    }

    public void SetNumericTarget(string str)
    {
        int value;
        try { value = int.Parse(str); }
        catch { value = 1; }
        editor.SetTargetNumericalValue(value);
    }

    public void SetTextTarget(string str)
    {
        editor.SetTargetTextValue(str);
    }
}
