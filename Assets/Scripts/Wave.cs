using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {
    public float timeBeforeWave = 2f;
    public List<EnemySpawnDetails> spawns;
    public Wave() {
        spawns = new List<EnemySpawnDetails>();
    }
}
