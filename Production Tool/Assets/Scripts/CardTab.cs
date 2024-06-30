using TMPro;
using UnityEngine;

public class CardTab : MonoBehaviour
{
    public bool ActiveTab;
    private int index;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject unsavedFlag;

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
}
