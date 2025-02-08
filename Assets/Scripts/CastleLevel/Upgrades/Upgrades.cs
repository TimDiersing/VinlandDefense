using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    private UpgradesPanel upgradesPanel;
    private BaseTurret[] allTurrets;
    private TurretItem[] playerLoadout;
    private int[] availableTurrets;
    private GameObject turretLoadoutPage;
    private GameObject turretUpgradesPage;
    private GameObject augmentsPage;
    private int currentPage;
    [SerializeField] private Transform[] turretButtons;
    private GameObject tabs;
    private TMP_Dropdown dropdown;
    private TurretSlot currentSlot;

    void Start() {
        playerLoadout = DataManager.Instance.GetPlayerLoadout();
        allTurrets = DataManager.Instance.GetAllTurrets();
        upgradesPanel = gameObject.GetComponent<UpgradesPanel>();
        currentSlot = null;
        currentPage = 0;
        availableTurrets = new int[DataManager.LOADOUT_SIZE];

        turretLoadoutPage = transform.Find("TurretLoadoutPage").gameObject;
        turretUpgradesPage = transform.Find("TurretUpgradesPage").gameObject;
        augmentsPage = transform.Find("AugmentsPage").gameObject;
        tabs = transform.Find("Tabs").gameObject;
        dropdown = turretUpgradesPage.transform.GetChild(0).GetComponent<TMP_Dropdown>();

        // Setup turret buttons
        turretLoadoutPage.SetActive(true);
        for (int i = 0; i < turretButtons.Length; i++) {
            if (playerLoadout[i] == null) {
                availableTurrets[i] = 0;
            } else {
                TurretsPageUnlockButton button = turretButtons[i].GetComponent<TurretsPageUnlockButton>();
                BaseTurret turret = allTurrets[playerLoadout[i].baseTurretIndex];
                button.SetTitle(turret.title);
                button.SetCost(turret.cost);
                button.SetImage(turret.UIImage);
                button.SetAmount(turret.amount);
                availableTurrets[i] = turret.amount;
            }
        }
        turretLoadoutPage.gameObject.SetActive(false);

    }


    public void TurretSlotClicked(TurretSlot turretSlot) {
        currentSlot = turretSlot;
        LoadCurrentPage();
        upgradesPanel.OpenPanel();
    }
    private void LoadTurretsPage() {
        
    }

    private void LoadTurretUpgradesPage() {
        dropdown.value = currentSlot.GetCurrentTurret().GetSearchType();
        turretUpgradesPage.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = "+$" + ((int)(currentSlot.GetTotalSpent() * DataManager.Instance.GetSellPercent())).ToString();
    }
    private void LoadAugmentsPage() {

    }

    private void LoadCurrentPage() {
        if (currentSlot.GetCurrentTurret() == null) {
            turretUpgradesPage.SetActive(false);
            turretLoadoutPage.SetActive(true);
            LoadTurretsPage();

        } else {
            turretLoadoutPage.SetActive(false);
            turretUpgradesPage.SetActive(true);
            LoadTurretUpgradesPage();
        }
    }

    public void BuyTurret(int turretButtonIndex) {

        BaseTurret baseTurret = allTurrets[playerLoadout[turretButtonIndex].baseTurretIndex];
        if (GameManager.Instance.GetGold() < baseTurret.cost) {
            Debug.Log("Not enough money to buy "+ baseTurret.name);
            return;
        }

        GameManager.Instance.ChangeGold(-baseTurret.cost);
        //availableTurrets[selectedTurret] -= 1;
        //turretButtons[selectedTurret].transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(availableTurrets[selectedTurret].ToString());

        // if (availableTurrets[selectedTurret] == 0) {
        //     turretButtons[selectedTurret].GetComponent<Button>().interactable = false;
        // }

        currentSlot.SetCurrentTurret(playerLoadout[turretButtonIndex], baseTurret);
        LoadCurrentPage();
    }

    public void SellTurret() {
        GameManager.Instance.ChangeGold(currentSlot.GetTotalSpent());
        currentSlot.RemoveTurret();
    }
}
