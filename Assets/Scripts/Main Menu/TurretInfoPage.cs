using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretInfoPage : MonoBehaviour
{   
    [SerializeField] Image turretImage;
    [SerializeField] TextMeshProUGUI turretTitle;
    [SerializeField] TextMeshProUGUI shardText;
    [SerializeField] RectTransform subskills;
    private TurretItem currentTurretItem;
    private BaseTurret currentBaseTurret;

    public void SetPage(TurretItem turret) {
        currentTurretItem = turret;
        currentBaseTurret = DataManager.Instance.GetTurretAtIndex(turret.baseTurretIndex);
        turretTitle.text = currentBaseTurret.title;
        turretImage.sprite = currentBaseTurret.UIImage;

        for (int i = 0; i < DataManager.SUBSKILL_AMT; i++) {
            Subskill subskill = DataManager.Instance.GetSubskill(currentTurretItem.subskillIndexs[i]);

            subskills.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(subskill.title);
            if (currentTurretItem.level < DataManager. SUBSKILL_UNLOCK_LEVELS[i]) {
                subskills.GetChild(i).GetChild(1).gameObject.SetActive(true);
            } else {
                subskills.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }
        }

        SetShardText();
    }

    public void SetShardText() {
        shardText.SetText(DataManager.Instance.GetShards(currentTurretItem.baseTurretIndex).ToString());
    }

    public void UpgradeTurret() {
        currentTurretItem.level += 1;

        for (int i = 0; i < DataManager.SUBSKILL_AMT; i++) {
            if (currentTurretItem.level == DataManager.SUBSKILL_UNLOCK_LEVELS[i]) {
                subskills.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    public void RecycleTurret() {

        DataManager.Instance.AddShards(currentBaseTurret.index, currentBaseTurret.recycleAmount);
        Actions.Instance.TurretRecycled(currentTurretItem.GetId());
        currentTurretItem = null;
        currentBaseTurret = null;

        gameObject.SetActive(false);
    }
}
