using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityEnemySpawner : MonoBehaviour
{
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
    public PhaseInfo[] phases_loop;

    private Dictionary<char, GameObject> enemyPrefabDict;

    private float mCurrentTime;
    struct SequenceItem
    {
        public char code;
        public string pathName;
        public float time;
    }
    private List<SequenceItem> mSequence;
    private List<SequenceItem> mSequence_loop;
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
            for (int j = 0; j < phases[i].enemyCode.Length; j++)
            {
                SequenceItem item = new SequenceItem();
                item.code = phases[i].enemyCode[j];
                item.time = phases[i].startTime + j * phases[i].intervalTime;
                item.pathName = phases[i].pathName;
                mSequence.Add(item);
            }
        }
        mSequence.Sort((a, b) => a.time.CompareTo(b.time));

        mSequence_loop = new List<SequenceItem>();
        for (int i = 0; i < phases_loop.Length; i++)
        {
            for (int j = 0; j < phases_loop[i].enemyCode.Length; j++)
            {
                SequenceItem item = new SequenceItem();
                item.code = phases_loop[i].enemyCode[j];
                item.time = phases_loop[i].startTime + j * phases_loop[i].intervalTime;
                item.pathName = phases_loop[i].pathName;
                mSequence_loop.Add(item);
            }
        }
        mSequence_loop.Sort((a, b) => a.time.CompareTo(b.time));
        mCurrentTime = 0;
    }
    private void Spawn(char code, string pathName)
    {
        GameObject enemy = Instantiate(enemyPrefabDict[code]);
        enemy.GetComponent<PathBehavior>().pathName = pathName;
    }
    private bool isSequenceLoop = false;
    private void Update()
    {
        mCurrentTime += Time.smoothDeltaTime;
        if (GameManager.sTheGlobalBehavior.mEnemyManager.IsEmpty())
        {
            mCurrentTime += Time.smoothDeltaTime * 1f; // Speed 1x up
        }
        if (!isSequenceLoop)
        {
            if (mCurrentSequenceIndex < mSequence.Count && mSequence[mCurrentSequenceIndex].time < mCurrentTime)
            {
                Spawn(mSequence[mCurrentSequenceIndex].code, mSequence[mCurrentSequenceIndex].pathName);
                mCurrentSequenceIndex++;
                if (mCurrentSequenceIndex >= mSequence.Count)
                {
                    isSequenceLoop = true;
                    mCurrentSequenceIndex = 0;
                    mCurrentTime = 0;
                }
            }
        }
        else if (mSequence_loop.Count > 0)
        {
            if (mCurrentTime > mSequence_loop[mCurrentSequenceIndex].time)
            {
                Spawn(mSequence_loop[mCurrentSequenceIndex].code, mSequence_loop[mCurrentSequenceIndex].pathName);
                mCurrentSequenceIndex++;
                if (mCurrentSequenceIndex >= mSequence_loop.Count)
                {
                    mCurrentSequenceIndex = 0;
                    mCurrentTime = 0;
                }
            }
        }
    }
}
