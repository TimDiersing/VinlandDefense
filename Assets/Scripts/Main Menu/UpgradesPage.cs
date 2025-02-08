using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesPage : MonoBehaviour
{
    private TextMeshProUGUI soulsText;
    private Transform startingGoldButton;
    private Transform startingLivesButton;
    [SerializeField] Button basicBoxButton;
    [SerializeField] Button superBoxButton;
    [SerializeField] Box basicBox;
    [SerializeField] Box superBox;
    [SerializeField] BoxOpening boxOpening;
    private DataManager dm;
    void Awake () {
        dm = DataManager.Instance;
        soulsText = transform.Find("SoulsIcon").GetChild(0).GetComponent<TextMeshProUGUI>();
        startingGoldButton = transform.Find("StartingGoldButton");
        startingLivesButton = transform.Find("StartingLivesButton");
    }

    public void SetPage() {

        // Set souls text off DataManager souls
        soulsText.SetText(DataManager.Instance.GetSouls().ToString());

        // Set Gold Button
        int[] goldInfo = dm.GetGoldShopInfo();
        if (goldInfo[2] == 0) {
            startingGoldButton.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("MAX");
            startingGoldButton.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("");
            startingGoldButton.GetChild(1).GetChild(0).gameObject.SetActive(false);
        } else {
            startingGoldButton.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(goldInfo[2].ToString());
            startingGoldButton.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("+" + goldInfo[1].ToString());
            startingLivesButton.GetChild(1).GetChild(0).gameObject.SetActive(true);
        }

        // Set Lives Button
        int[] livesInfo = dm.GetLivesShopInfo();
        if (livesInfo[2] == 0) {
            startingLivesButton.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("MAX");
            startingLivesButton.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("");
            startingLivesButton.GetChild(1).GetChild(0).gameObject.SetActive(false);
        } else {
            startingLivesButton.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(livesInfo[2].ToString());
            startingLivesButton.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("+" + livesInfo[1].ToString());
            startingLivesButton.GetChild(1).GetChild(0).gameObject.SetActive(true);
        }

        // Set Box Buttons
        basicBoxButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(basicBox.cost.ToString());
        superBoxButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(superBox.cost.ToString());

        RefreshButtons();
    }

    public void UpgradeGold() {
        dm.UpgradeGold();
        soulsText.SetText(dm.GetSouls().ToString());

        // Set Gold Button
        int[] goldInfo = dm.GetGoldShopInfo();
        if (goldInfo[2] == 0) {
            startingGoldButton.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("MAX");
            startingGoldButton.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("");
            startingGoldButton.GetChild(1).GetChild(0).gameObject.SetActive(false);
        } else {
            startingGoldButton.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(goldInfo[2].ToString());
            startingGoldButton.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("+" + goldInfo[1].ToString());
        }

        RefreshButtons();
    }

    public void UpgradeLives() {
        dm.UpgradeLives();
        soulsText.SetText(dm.GetSouls().ToString());

        // Set Lives Button
        int[] livesInfo = dm.GetLivesShopInfo();
        if (livesInfo[2] == 0) {
            startingLivesButton.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("MAX");
            startingLivesButton.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("");
            startingLivesButton.GetChild(1).GetChild(0).gameObject.SetActive(false);
        } else {
            startingLivesButton.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(livesInfo[2].ToString());
            startingLivesButton.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("+" + livesInfo[1].ToString());
        }

        RefreshButtons();
    }

    public void BuyBox(Box box) {
        
        if (DataManager.Instance.GetSouls() < box.cost) {
            Debug.Log("Not enough money for " + box.boxName);
            return;
        }

        dm.ChangeSouls(-box.cost);
        soulsText.SetText(dm.GetSouls().ToString());
        RefreshButtons();

        boxOpening.OpenBox(box);
    }
    
    public void RefreshButtons() {
        int[] costs = dm.GetCurrentCosts();
        int currentSouls = dm.GetSouls();

        if (costs[0] > currentSouls || costs[0] == 0) {
            startingGoldButton.GetComponent<Button>().interactable = false;
        } else {
            startingGoldButton.GetComponent<Button>().interactable = true;
        }

        if (costs[1] > currentSouls || costs[1] == 0) {
            startingLivesButton.GetComponent<Button>().interactable = false;
        } else {
            startingLivesButton.GetComponent<Button>().interactable = true;
        }

        if (basicBox.cost > currentSouls) {
            basicBoxButton.interactable = false;
        } else {
            basicBoxButton.interactable = true; 
        }

        if (superBox.cost > currentSouls) {
            superBoxButton.interactable = false;
        } else {
            superBoxButton.interactable = true; 
        }
    }
    public void AddSouls(int souls) {
        dm.ChangeSouls(souls);
        soulsText.SetText(dm.GetSouls().ToString());

        RefreshButtons();
    }
    public void ResetAllProgress() {
        dm.ResetAll();

        SetPage();
    }
}
