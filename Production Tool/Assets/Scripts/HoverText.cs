using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverText : Selectable
{
    [SerializeField] private GameObject hoverTextPrefab;
    private GameObject currentHoverText;
    public string content;
    [SerializeField] private float maxWidth = 600;
    public static Transform hoverTextParent;

    private bool hoverActive = false;
    private float hoverTime = 0;
    public float timeThreshold = 1.0f;

    protected override void Awake()
    {
        base.Awake();
        if (hoverTextParent != null) { return; }
        hoverTextParent = FindObjectOfType<HoverTextParent>().transform;
    }

    private void Update()
    {
        Debug.Log(IsHighlighted());
        if (IsHighlighted())
        {
            if(Input.GetMouseButtonDown(0))
            {
                hoverActive = true;
                if(currentHoverText != null)
                { Destroy(currentHoverText); }
            }

            if (hoverActive) { return; }
            hoverTime += Time.deltaTime;
            if (hoverTime > timeThreshold)
            {
                StartCoroutine(CreateHoverText());
                hoverActive = true;
                hoverTime = 0;
            }
            return;
        }
        else if (hoverActive)
        {
            hoverActive = false;
            Destroy(currentHoverText);
            return;
        }
        hoverTime = 0;
    }

    private IEnumerator CreateHoverText()
    {
        currentHoverText = Instantiate(hoverTextPrefab, hoverTextParent);
        currentHoverText.transform.position = Input.mousePosition;
        TMP_Text text = currentHoverText.GetComponentInChildren<TMP_Text>();
        text.text = content;
        RectTransform rect = (RectTransform)currentHoverText.transform;
        yield return null;
        Debug.Log("Setting hovertext bounds: " + text.textBounds.size);
        rect.sizeDelta = (Vector2)text.textBounds.size + new Vector2(22, 22);

    }








}
