using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public HashSet<GameObject> mEnemies;
    /* Spawn Parts */
    [System.Serializable]
    public struct EnemyInfo
    {
        public GameObject enemyPrefab;
        public string pathName;
        public int totalSpawn;
        public float cooldownTime;
    }
    [System.Serializable]
    public struct PhaseInfo
    {
        public EnemyInfo[] enemies;
    }
    public PhaseInfo[] phases;

    private float[] lastSpawnTime = null;
    private int[] numSpawn = null;
    private EnemyInfo[] enemyToSpawn = null;
    private bool spawning = false;
    private int numPhaseInfo;
    private int numEnemyInfo;

    public void AddEnemy(GameObject enemy) {
        mEnemies.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy) {
        mEnemies.Remove(enemy);
    }
    public void RemoveAllEnemies() {
        mEnemies.Clear();
    }
    public GameObject GetClosestEnemy(Vector3 position) {
        GameObject closest = null;
        float closestDistance = float.MaxValue;
        foreach (GameObject enemy in mEnemies) {
            float distance = (enemy.transform.position - position).magnitude;
            if (distance < closestDistance) {
                closest = enemy;
                closestDistance = distance;
            }
        }
        return closest;
    }
    private void Start()
    {
        mEnemies = new HashSet<GameObject>();
        numPhaseInfo = phases.Length;
        // test spawning
        StartPhase(1);
    }
    private void Update()
    {
        if (spawning) Spawn();
    }
    public void StartPhase(int index)
    {
        Debug.Assert(index >= 0 && index < numPhaseInfo);

        enemyToSpawn = phases[index].enemies;
        numEnemyInfo = enemyToSpawn.Length;

        if (numEnemyInfo > 0)
        {
            lastSpawnTime = new float[numEnemyInfo];
            numSpawn = new int[numEnemyInfo];
            float current = Time.time;
            for (int i = 0; i < numEnemyInfo; i++)
            {
                lastSpawnTime[i] = current;
                numSpawn[i] = 0;
            }

            spawning = true;
        }
    }
    private void Spawn()
    {
        Debug.Assert(numSpawn != null && lastSpawnTime != null);

        spawning = false;

        for (int i = 0; i < enemyToSpawn.Length; i++)
        {
            // check number
            if (numSpawn[i] < enemyToSpawn[i].totalSpawn)
            {
                spawning = true;
                // check cooldown
                float current = Time.time;
                if (current - lastSpawnTime[i] >= enemyToSpawn[i].cooldownTime * Time.smoothDeltaTime)
                {
                    Debug.Log(current);
                    GameObject e = Instantiate(enemyToSpawn[i].enemyPrefab);
                    EnemyBehavior eb = e.GetComponent<EnemyBehavior>();
                    Debug.Assert(eb != null);
                    eb.SetPath(enemyToSpawn[i].pathName);

                    lastSpawnTime[i] = current;
                    numSpawn[i]++;
                }
            }
        }
    }
}