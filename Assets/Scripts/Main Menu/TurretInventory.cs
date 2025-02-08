using UnityEngine;
using UnityEngine.EventSystems;

public class TurretInventory : MonoBehaviour, IDropHandler
{
    [SerializeField] RectTransform contentTransform;
    [SerializeField] GameObject turretInventoryItem;

    private void Start() {

        
    }
    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag != null) {
            eventData.pointerDrag.GetComponent<RectTransform>().SetParent(contentTransform);
        }
    }
}
