using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HoverText : MonoBehaviour
{
    private Bounds bounds;
    [SerializeField] private GameObject hoverTextPrefab;
    private GameObject currentHoverText;
    public string content;
    public static Transform hoverTextParent;
    public LayerMask hitboxLayer;

    private bool hoverActive = false;
    private float hoverTime = 0;
    public float timeThreshold = 1.0f;

    private void Awake()
    {
        bounds = GetComponent<BoxCollider>().bounds;
        if (hoverTextParent != null) { return; }
        hoverTextParent = FindObjectOfType<HoverTextParent>().transform;
    }

    private void Update()
    {
        if (IsHoveredOver())
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
        hoverActive = false;
        hoverTime = 0;
    }

    private IEnumerator CreateHoverText()
    {
        currentHoverText = Instantiate(hoverTextPrefab, hoverTextParent);
        currentHoverText.transform.position = Input.mousePosition;
        TMP_Text text = currentHoverText.GetComponentInChildren<TMP_Text>();
        RectTransform textRect = (RectTransform)text.transform;
        RectTransform rect = (RectTransform)currentHoverText.transform;
        float textWidth = Mathf.Min(textRect.rect.size.x, Display.main.renderingWidth - textRect.position.x);
        textRect.sizeDelta = new Vector2(textWidth, textRect.rect.size.y);
        text.text = content;
        yield return null;
        Debug.Log("Setting hovertext bounds: " + text.textBounds.size);
        rect.sizeDelta = (Vector2)text.textBounds.size + new Vector2(22, 22);

    }

    private Vector3 previousMousePos = new();
    private bool IsHoveredOver()
    {
        bool result = PointInBounds(Input.mousePosition) && gameObject.activeInHierarchy && previousMousePos == Input.mousePosition;
        previousMousePos = Input.mousePosition;
        return result;
    }

    private bool PointInBounds(Vector2 point)
    {
        if(point.x < bounds.min.x || point.x > bounds.max.x || point.y < bounds.min.y || point.y > bounds.max.y) { return false; }
        return true;
    }








}
