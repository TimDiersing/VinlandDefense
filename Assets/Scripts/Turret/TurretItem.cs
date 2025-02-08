/*
    A turret item that a player can obtain that holds a base turret and the subskills they got with it. As well as any upgrades or modifications.
*/

[System.Serializable]
public class TurretItem {
    public int baseTurretIndex;
    public int loadoutIndex;
    public int level;
    public int[] subskillIndexs;
    private int turretID;
    
    public TurretItem(int baseTurretIndex, int[] subskills, int id) {
        this.baseTurretIndex = baseTurretIndex;
        this.subskillIndexs =  subskills;
        loadoutIndex = -1;
        level = 1;
        turretID = id;
    }

    public int GetId() {
        return turretID;
    }
   
}