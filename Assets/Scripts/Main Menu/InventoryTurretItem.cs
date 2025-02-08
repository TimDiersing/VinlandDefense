using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryTurretItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startingPosition;
    private TurretItem turretItem;
    private TurretInfoPage turretInfoPage;
    private int latestPosition;

    private void Awake() {
        Actions.Instance.onTurretRecycled += ItemRecycled;
    }
    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        latestPosition = -1;

        if (turretItem != null) {
            rectTransform.GetChild(0).GetComponent<Image>().sprite = DataManager.Instance.GetTurretAtIndex(turretItem.baseTurretIndex).UIImage;
        }
    }

    public void SetItem(TurretItem turretItem, Canvas canvas, TurretInfoPage turretInfoPage) {
        this.canvas = canvas;
        this.turretInfoPage = turretInfoPage;
        this.turretItem = turretItem;
    }

    public void OnPointerClick(PointerEventData eventData) {
        turretInfoPage.SetPage(turretItem);
        turretInfoPage.gameObject.SetActive(true);
    }
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("OnPointerDown");
        startingPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        
        if (rectTransform.parent.CompareTag("TurretInventory")) {
            rectTransform.anchoredPosition = startingPosition;
            turretItem.loadoutIndex = -1;
            if (latestPosition != -1) {
                latestPosition = -1;
            }
        } else if (rectTransform.parent.CompareTag("LoadoutSlot")) {
            rectTransform.anchoredPosition = new Vector2(0f, 0f);
            LoadoutSlot loadoutSlot = rectTransform.parent.GetComponent<LoadoutSlot>();
            latestPosition = loadoutSlot.GetSlotNumber();
            turretItem.loadoutIndex = loadoutSlot.GetSlotNumber();
            
        } else {
            Debug.Log("Inventory item child of wrong thing");
        }
    }

    
    public TurretItem GetTurret() {
        return turretItem;
    }

    public void ItemRecycled(int id) {
        if (turretItem.GetId() == id) {
            Destroy(gameObject);
        }
    }

    public void OnDestroy() {
        Actions.Instance.onTurretRecycled -= ItemRecycled;
    }

}
