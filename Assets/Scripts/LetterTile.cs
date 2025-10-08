using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LetterTile : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public char letterChar;
    public TextMeshProUGUI letterText;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Vector3 startPos;
    private WordManager wordManager;

    public void Setup(char c, WordManager wm)
    {
        letterChar = c;
        wordManager = wm;
        letterText.text = c.ToString();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        startPos = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Simple snap-back for prototype
        rectTransform.anchoredPosition = startPos;
    }
}

