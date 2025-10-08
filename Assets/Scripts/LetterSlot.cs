using UnityEngine;
using UnityEngine.EventSystems;

public class LetterSlot : MonoBehaviour, IDropHandler
{
    public LetterTile currentTile;

    public void OnDrop(PointerEventData eventData)
    {
        if (currentTile != null)
        {
            // Slot already occupied
            return;
        }

        LetterTile droppedTile = eventData.pointerDrag.GetComponent<LetterTile>();
        if (droppedTile != null)
        {
            currentTile = droppedTile;
            droppedTile.transform.SetParent(transform);
            droppedTile.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

    public char GetLetter()
    {
        if (currentTile != null)
            return currentTile.letterChar;
        else
            return '_'; // Empty slot
    }

    public void ClearSlot()
    {
        currentTile = null;
    }
}

