using UnityEngine;
using UnityEngine.EventSystems;

public class TurretSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Upgrades upgrades;
    private Turret currentTurret;
    private TurretItem turretItem;
    private BaseTurret baseTurret;
    private int totalSpent;

    private void Start() {
        currentTurret = null;
        totalSpent = 0;
    }
    public void OnPointerClick(PointerEventData eventData) {
        upgrades.TurretSlotClicked(this);
    }

    public Turret GetCurrentTurret() {
        return currentTurret;
    }

    public void SetCurrentTurret(TurretItem turretItem, BaseTurret baseTurret) {
        if (currentTurret != null) {
            Destroy(currentTurret.gameObject);
        }
        this.turretItem = turretItem;
        this.baseTurret = baseTurret;

        totalSpent += baseTurret.cost;
        currentTurret = Instantiate(baseTurret.turretPF, transform).GetComponent<Turret>();

        currentTurret.IncreaseDmg(baseTurret.dmgPerLevel * turretItem.level);

        Subskill[] subskills = new Subskill[DataManager.SUBSKILL_AMT];
        for (int i = 0; i < DataManager.SUBSKILL_AMT; i++) {
            if (turretItem.level >= DataManager.SUBSKILL_UNLOCK_LEVELS[i]) {
                subskills[i] = DataManager.Instance.GetSubskill(turretItem.subskillIndexs[i]);
            } else {
                subskills[i] = null;
            }
        }

        // Adding subskills
        for (int i = 0; i < DataManager.SUBSKILL_AMT; i++) {
            if (subskills[i] == null) {
                continue;
            }
            Debug.Log(subskills[i].skill);

            switch (subskills[i].skill) {
                case Skill.AddDamage:
                    currentTurret.IncreaseDmg(subskills[i].skillValue);
                    break;
                case Skill.GoldInc:
                    GameManager.Instance.AddToGoldMult(subskills[i].skillValue);
                    break;
            }
        }

        // multiplying subskills
        for (int i = 0; i < DataManager.SUBSKILL_AMT; i++) {
            if (subskills[i] == null) {
                continue;
            }

            switch (subskills[i].skill) {
                case Skill.MultDamage:
                    currentTurret.MultDmg(subskills[i].skillValue);
                    break;
            }
        }
    }
    public void RemoveTurret() {
        Destroy(currentTurret.gameObject);
        currentTurret = null;
        turretItem = null;
        baseTurret = null;
        totalSpent = 0;
    }
    public int GetTotalSpent() {
        return totalSpent;
    }
}
