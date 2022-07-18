using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public HashSet<GameObject> mEnemies;
    /* Spawn Parts */
    [System.Serializable]
    public struct EnemyPrefab
    {
        public char code;
        public GameObject prefab;
    }

    [System.Serializable]
    public struct PhaseInfo
    {
        public string enemyCode;
        public string pathName;
        public float intervalTime;
        public float startTime;
    }

    public EnemyPrefab[] enemyPrefabs;
    public PhaseInfo[] phases;

    private Dictionary<char, GameObject> enemyPrefabDict;

    private float lastSpawnTime;
    private int indexSpawn;
    private int indexPhase;

    private GameObject[] enemyToSpawn;
    private string pathName;
    private float intervalTime;
    private float phaseTimer;
    private bool spawning = false;
    private bool levelEnd = false;

    public void AddEnemy(GameObject enemy)
    {
        mEnemies.Add(enemy);
    }
    public void RemoveEnemy(GameObject enemy)
    {
        mEnemies.Remove(enemy);
    }
    public void RemoveAllEnemies()
    {
        mEnemies.Clear();
    }
    public GameObject GetClosestEnemy(Vector3 position)
    {
        GameObject closest = null;
        float closestDistance = float.MaxValue;
        foreach (GameObject enemy in mEnemies)
        {
            float distance = (enemy.transform.position - position).magnitude;
            if (distance < closestDistance)
            {
                closest = enemy;
                closestDistance = distance;
            }
        }
        return closest;
    }
    private void Awake()
    {
        mEnemies = new HashSet<GameObject>();
    }
    private void Start()
    {
        enemyPrefabDict = new Dictionary<char, GameObject>();

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            if (!enemyPrefabDict.ContainsKey(enemyPrefabs[i].code))
            {
                enemyPrefabDict.Add(enemyPrefabs[i].code, enemyPrefabs[i].prefab);
            }
        }

        indexPhase = 0;
        phaseTimer = 0.0f;
    }
    private void Update()
    {
        if (indexPhase >= phases.Length)
        {
            if (!levelEnd)
            {
                Debug.Log("End");
                levelEnd = true;
            }
        }
        else
        {
            if (phaseTimer >= phases[indexPhase].startTime && !spawning)
            {
                Debug.Log("Phase:" + indexPhase.ToString());
                StartPhase();
            }

            if (spawning)
            {
                Spawn();
            }
            else
            {
                if (mEnemies.Count <= 0)
                {
                    // Accerate the next wave
                    phaseTimer += Time.deltaTime;
                }
            }
            phaseTimer += Time.deltaTime;
        }
    }
    public void StartPhase()
    {
        string codes = phases[indexPhase].enemyCode;
        enemyToSpawn = new GameObject[codes.Length];
        for (int i = 0; i < codes.Length; i++)
        {
            if (enemyPrefabDict.ContainsKey(codes[i]))
            {
                enemyToSpawn[i] = enemyPrefabDict[codes[i]];
            }
            else enemyToSpawn[i] = null;
        }

        pathName = phases[indexPhase].pathName;
        intervalTime = phases[indexPhase].intervalTime;
        spawning = true;
        indexSpawn = 0;
        lastSpawnTime = Time.time;
    }
    private void Spawn()
    {
        if (indexSpawn >= enemyToSpawn.Length)
        {
            spawning = false;
            indexPhase++;
            return;
        }

        if (Time.time - lastSpawnTime >= intervalTime)
        {
            GameObject e = Instantiate(enemyToSpawn[indexSpawn]);
            PathBehavior pb = e.GetComponent<PathBehavior>();
            Debug.Assert(pb != null);
            pb.pathName = pathName;
            indexSpawn++;
            lastSpawnTime = Time.time;
        }
    }
}