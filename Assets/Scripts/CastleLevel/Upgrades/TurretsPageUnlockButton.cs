using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretsPageUnlockButton : MonoBehaviour
{
    private TextMeshProUGUI titleText;
    private TextMeshProUGUI costText;
    private TextMeshProUGUI amountText;
    private Image turretImage;
    private int turretAmount;
    private int turretCost;
    private void Awake() {
        titleText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        costText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        amountText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        turretImage = transform.GetChild(2).GetComponent<Image>();

        Actions.Instance.onGoldChanged += UpdateEnable;

        gameObject.GetComponent<Button>().interactable = false;
    }

    public void SetTitle(String title) {
        titleText.SetText(title);
    }

    public void SetCost(int cost) {
        costText.SetText(cost.ToString());
        turretCost = cost;

        Debug.Log("Gold: " + GameManager.Instance.GetGold() + "Cost: " + cost);
        if (GameManager.Instance.GetGold() >= cost) {
            gameObject.GetComponent<Button>().interactable = true;
        }
    }

    public void SetImage(Sprite image) {
        turretImage.sprite = image;
    }

    public void SetAmount(int amount) {
        turretAmount = amount;
        amountText.SetText(amount.ToString());

        if (turretAmount <= 0) {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

    public void IncreaseAmount() {
        turretAmount += 1;
        amountText.SetText(turretAmount.ToString());
        gameObject.GetComponent<Button>().interactable = true;
    }

    public void ReduceAmount() {
        if (turretAmount <= 0) {
            Debug.Log("TurretsPageUnlockButton is trying to reduce turret amount when none left");
            return;
        }

        turretAmount -= 1;
        amountText.SetText(turretAmount.ToString());
        if (turretAmount == 0) {
            gameObject.GetComponent<Button>().interactable = false;
            Actions.Instance.onGoldChanged -= UpdateEnable;
        }
    }

    private void UpdateEnable() {
        if (GameManager.Instance.GetGold() < turretCost) {
            gameObject.GetComponent<Button>().interactable = false;
        } else {
            gameObject.GetComponent<Button>().interactable = true;
        }
    }

    private void OnDestroy() {
        Actions.Instance.onGoldChanged -= UpdateEnable;
    }
}
