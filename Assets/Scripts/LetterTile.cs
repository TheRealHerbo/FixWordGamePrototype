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
    private Transform slot;
    private Vector2 lastAnchoredPos;
    private WordManager wordManager;

    public void Setup(char c, WordManager wm)
    {
        letterChar = c;
        if (letterText != null) letterText.text = c.ToString();
        wordManager = wm;

        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();

        slot = transform.parent;
        if (rectTransform != null)
            lastAnchoredPos = rectTransform.anchoredPosition;
        else
            lastAnchoredPos = Vector2.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }

        if (canvas != null)
        {
            transform.SetParent(canvas.transform, true);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (rectTransform == null || canvas == null) return;

        // move in UI space
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }

        // find a slot under the pointer among the managed slots
        Transform foundSlot = null;
        foreach (Transform slot in wordManager.letterParent)
        {
            RectTransform slotRt = slot as RectTransform;
            if (slotRt == null) continue;

            Camera cam = (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceCamera) ? canvas.worldCamera : null;
            if (RectTransformUtility.RectangleContainsScreenPoint(slotRt, eventData.position, cam))
            {
                foundSlot = slot;
                break;
            }
        }

        if (foundSlot != null)
        {
            LetterTile other = foundSlot.GetComponentInChildren<LetterTile>();

            transform.SetParent(foundSlot, false);
            rectTransform.anchoredPosition = Vector2.zero;

            if (other != null && other != this)
            {
                other.transform.SetParent(slot, false);
                RectTransform otherRt = other.GetComponent<RectTransform>();
                if (otherRt != null)
                    otherRt.anchoredPosition = Vector2.zero;

                other.slot = slot;
                slot = foundSlot;
            }

        }
        else
        {
            transform.SetParent(slot, false);
            if (rectTransform != null)
                rectTransform.anchoredPosition = lastAnchoredPos;
        }
    }
}
