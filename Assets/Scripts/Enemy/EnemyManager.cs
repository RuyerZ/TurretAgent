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
    private List<GameObject> enemiesInRadius = new List<GameObject>();

    private float mCurrentTime;

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
    public List<GameObject> GetEnemiesInRadius(Vector2 center, float radius)
    {
        enemiesInRadius.Clear();
        foreach (GameObject enemy in mEnemies)
        {
            Vector2 enemyPos = enemy.transform.position;
            float distance = (enemyPos - center).magnitude;
            if (distance < radius)
            {
                enemiesInRadius.Add(enemy);
            }
        }
        return enemiesInRadius;
    }
    private void Awake()
    {
        mEnemies = new HashSet<GameObject>();
    }
    struct SequenceItem {
        public char code;
        public string pathName;
        public float time;
    }
    private List<SequenceItem> mSequence;
    private int mCurrentSequenceIndex;
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
        mSequence = new List<SequenceItem>();
        mCurrentSequenceIndex = 0;
        for (int i = 0; i < phases.Length; i++)
        {
            for (int j = 0;j<phases[i].enemyCode.Length;j++)
            {
                SequenceItem item = new SequenceItem();
                item.code = phases[i].enemyCode[j];
                item.time = phases[i].startTime + j * phases[i].intervalTime;
                item.pathName = phases[i].pathName;
                mSequence.Add(item);
            }
        }
        mSequence.Sort((a, b) => a.time.CompareTo(b.time));
        mCurrentTime = 0;
    }
    private void Spawn(char code,string pathName) {
        GameObject enemy = Instantiate(enemyPrefabDict[code]);
        enemy.GetComponent<PathBehavior>().pathName = pathName;
    }
    private void Update() {
        mCurrentTime += Time.smoothDeltaTime;
        if (mEnemies.Count == 0)
        {
            mCurrentTime += Time.smoothDeltaTime*1f; // Speed 1x up
        }
        if (mCurrentSequenceIndex < mSequence.Count && mSequence[mCurrentSequenceIndex].time < mCurrentTime)
        {
            Spawn(mSequence[mCurrentSequenceIndex].code, mSequence[mCurrentSequenceIndex].pathName);
            mCurrentSequenceIndex++;
        }
        
        if (mCurrentSequenceIndex == mSequence.Count && mEnemies.Count == 0)
        {
            GameManager.sTheGlobalBehavior.GameWin();
        }
    }
}