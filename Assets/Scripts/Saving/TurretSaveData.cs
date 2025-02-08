
using System.Collections.Generic;

[System.Serializable]
public class TurretSaveData {
    public List<TurretItem> playersTurrets;
    public int[] turretShards;
    public int turretsCreated;

    public TurretSaveData(List<TurretItem> playersTurrets, int[] turretShards, int turretsCreated) {
        this.playersTurrets = playersTurrets;
        this.turretShards = turretShards;
        this.turretsCreated = turretsCreated;
    }
}