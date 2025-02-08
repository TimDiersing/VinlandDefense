using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxOpening : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rarityText;
    [SerializeField] Image unlockedImage;
    [SerializeField] TextMeshProUGUI unlockedTitle;
    private BaseTurret[] allTurrets;

    private void Start() {
        allTurrets = DataManager.Instance.GetAllTurrets();
    }

    public void OpenBox(Box box) {

        float rollNumber = Random.Range(0f, 1f);
        if (rollNumber > box.rareOdds + box.epicOdds + box.legnerdayOdds) {
            // Common
            TurretItem turretUnlocked = DataManager.Instance.UnlockRandomCommonTurret();
            BaseTurret baseTurret = allTurrets[turretUnlocked.baseTurretIndex];
            rarityText.SetText("common");
            rarityText.color = Color.cyan;
            unlockedImage.sprite = baseTurret.UIImage;
            unlockedTitle.SetText(baseTurret.title);

        } else if (rollNumber > box.epicOdds + box.legnerdayOdds) {
            // Rare
            TurretItem turretUnlocked = DataManager.Instance.UnlockRandomRareTurret();
            BaseTurret baseTurret = allTurrets[turretUnlocked.baseTurretIndex];
            rarityText.SetText("rare");
            rarityText.color = Color.blue;
            unlockedImage.sprite = baseTurret.UIImage;
            unlockedTitle.SetText(baseTurret.title);

        } else if (rollNumber > box.legnerdayOdds) {
            // Epic
            TurretItem turretUnlocked = DataManager.Instance.UnlockRandomEpicTurret();
            BaseTurret baseTurret = allTurrets[turretUnlocked.baseTurretIndex];
            rarityText.SetText("epic");
            rarityText.color = Color.magenta;
            unlockedImage.sprite = baseTurret.UIImage;
            unlockedTitle.SetText(baseTurret.title);

        } else {
            // Legenday
            TurretItem turretUnlocked = DataManager.Instance.UnlockRandomLegendaryTurret();
            BaseTurret baseTurret = allTurrets[turretUnlocked.baseTurretIndex];
            rarityText.SetText("legendary");
            rarityText.color = Color.yellow;
            unlockedImage.sprite = baseTurret.UIImage;
            unlockedTitle.SetText(baseTurret.title);

        }
    }
}
