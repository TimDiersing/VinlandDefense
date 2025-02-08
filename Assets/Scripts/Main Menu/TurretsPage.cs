using System.Collections.Generic;
using UnityEngine;

public class TurretsPage : MonoBehaviour
{
    [SerializeField] GameObject inventoryTurretItem;
    private RectTransform loadoutSlots;
    private RectTransform turretInventory;
    private Canvas canvas;
    private TurretInfoPage turretInfoPage;


    private void Awake() {
        loadoutSlots = GetComponent<RectTransform>().Find("LoadoutSlots").GetComponent<RectTransform>();
        turretInventory = GetComponent<RectTransform>().Find("TurretInventory").GetChild(0).GetChild(0).GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        turretInfoPage = GetComponent<RectTransform>().Find("TurretInfo").GetComponent<TurretInfoPage>();
        Actions.Instance.onTurretUnlocked += AddTurretItem;

        SetPage();
    }

    public void SetPage() {
        List<TurretItem> playersTurrets = DataManager.Instance.GetPlayersTurrets();
        for (int i = 0; i < playersTurrets.Count; i++) {
            GameObject turretItem = Instantiate(inventoryTurretItem, turretInventory);
            turretItem.GetComponent<InventoryTurretItem>().SetItem(playersTurrets[i], canvas, turretInfoPage);
            if (playersTurrets[i].loadoutIndex >= 0) {
                turretItem.GetComponent<RectTransform>().SetParent(loadoutSlots.GetChild(playersTurrets[i].loadoutIndex).GetComponent<RectTransform>());
                turretItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
            }
        }
    }

    public void AddTurretItem(TurretItem turretItem) {
        GameObject item = Instantiate(inventoryTurretItem, turretInventory);
        item.GetComponent<InventoryTurretItem>().SetItem(turretItem, canvas, turretInfoPage);
    }

    private void OnDestroy() {
        Actions.Instance.onTurretUnlocked -= AddTurretItem;
    }
}
