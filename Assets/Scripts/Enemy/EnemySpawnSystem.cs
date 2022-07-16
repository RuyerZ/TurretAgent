using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnSystem : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        numPhaseInfo = phases.Length;

        StartPhase(1);
    }

    // Update is called once per frame
    void Update()
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
