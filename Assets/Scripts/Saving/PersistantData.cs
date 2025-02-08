using UnityEngine;

[System.Serializable]
public class PersistantData {
    public int soulsCount;
    public int levelsBeaten;
    public int startingGoldLevel;
    public int startingLivesLevel;


    public PersistantData(int soulsCount, int levelsBeaten, int startingGoldLevel, int startingLivesLevel) {
        this.soulsCount = soulsCount;
        this.levelsBeaten = levelsBeaten;
        this.startingGoldLevel = startingGoldLevel;
        this.startingLivesLevel = startingLivesLevel;
    }
}
