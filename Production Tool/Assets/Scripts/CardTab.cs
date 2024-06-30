using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardTab : MonoBehaviour
{
    public bool IsUnsaved { get { return unsavedFlag.activeSelf; } }
    private int index;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject unsavedFlag;
    [SerializeField] private Image background;
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color activeColor;


    public void Setup(int _index, Vector2 _offset)
    {
        index = _index;
        transform.Translate(_offset);
    }

    public void Click()
    {
        CardDataManager.Instance.SetActiveIndex(index);
    }

    public void SetTabName(string cardName)
    {
        text.text = cardName;
    }

    public void SetUnsavedFlag(bool flag)
    {
        unsavedFlag.SetActive(flag);
    }

    public void SetTabActive(bool active)
    {
        background.color = active ? activeColor : inactiveColor;
    }
}
