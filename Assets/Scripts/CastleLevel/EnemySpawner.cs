using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const int MAX_SPAWNING_UNITS = 5;
    private const float TIME_AFTER_WAVES = 2f;
    private const float CHECK_TIME = .5f;
    [SerializeField] Level level;
    [SerializeField] float spawnRange;
    [SerializeField] TextMeshProUGUI waveText;

    private Coroutine[] spawners;
    private int[] spawnersRunning;
    private bool testingForEnemys;
    private float checkTimer;
    private Transform allEnemys;
    private GameManager gm;
    private int waveIndex;


    void Start() {

        allEnemys = GameObject.Find("Enemys").transform;
        gm = GameManager.Instance;

        // Display the wave starting text
        waveText.text = "Wave: 1/" + level.waves.Count;

        waveIndex = 0;
        StartCoroutine(StartWave());
    }

    void Update() {

        // When the wave is no longer spawning we check to see if enemys are left
        if (testingForEnemys) {

            if (checkTimer <= 0) {
                checkTimer = CHECK_TIME;

                // Then the wave is over
                if (allEnemys.childCount ==  0) { 
                    testingForEnemys = false;
                    waveIndex++;
                    if (waveIndex < level.waves.Count) {
                        StartCoroutine(StartWave());
                    } else {
                        gm.GameOver(true, level.levelNumber);
                    }
                }
            }

            checkTimer -= Time.deltaTime;
        }
    }

    IEnumerator StartWave() {
        yield return new WaitForSeconds(TIME_AFTER_WAVES);

        // Display the wave starting text
        waveText.text = "Wave: " + (waveIndex + 1) + "/" + level.waves.Count;
        // waveText.gamgeObject.GetComponent<Animator>().SetT

        // Set up the wave
        List<EnemySpawnDetails> waveSpawns = level.waves[waveIndex].spawns;
        spawners = new Coroutine[waveSpawns.Count];
        spawnersRunning = new int[waveSpawns.Count];

        yield return new WaitForSeconds(level.waves[waveIndex].timeBeforeWave);

        for (int i = 0; i < waveSpawns.Count; i++) {
            spawners[i] = StartCoroutine(SpawnEnemyType(waveSpawns[i], i));
            spawnersRunning[i] = 1;
        }
    }

    IEnumerator SpawnEnemyType(EnemySpawnDetails enemy, int thisIndex) {

        yield return new WaitForSeconds(enemy.timeBeforeSpawn);

        for (int i = 0; i < enemy.amt; i++) {
            SpawnEnemy(enemy.enemyPF);
            yield return new WaitForSeconds(1 / enemy.spawnRate);
        }

        CheckSpawners(thisIndex);
    }

    // Check to see if any spawners are still going
    private void CheckSpawners(int thisIndex) {
        spawnersRunning[thisIndex] = 0;
        bool doneSpawning = true;
        for (int i = 0; i < spawnersRunning.Length; i++) {
            if (spawnersRunning[i] == 1) {
                doneSpawning = false;
            }
        }

        // If all spawners are done for the wave
        if (doneSpawning) {
            testingForEnemys = true;
        }
    }

    private void SpawnEnemy(GameObject enemyPF) {
        GameObject enemySpawned = Instantiate(enemyPF, transform);
        enemySpawned.transform.position += new Vector3(Random.Range(-spawnRange, spawnRange), 0, 0);
    }
}
