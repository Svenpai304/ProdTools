using UnityEngine;
using UnityEngine.Events;

public class IntSetter : MonoBehaviour
{
    private int value;
    public UnityEvent<int> OnValueChanged;

    public void SetIntFromString(string str)
    {
        try { value = int.Parse(str); }
        catch { value = 1; }
        OnValueChanged?.Invoke(value);
    }
}
