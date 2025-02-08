using UnityEngine;
using UnityEngine.EventSystems;

public class LoadoutSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] int slotNumber;
    RectTransform rectTransform;
    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("Dropped");
        if (eventData.pointerDrag != null) {
            eventData.pointerDrag.GetComponent<RectTransform>().SetParent(rectTransform);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            eventData.pointerDrag.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        }
    }

    public int GetSlotNumber() {
        return slotNumber;
    }
}
