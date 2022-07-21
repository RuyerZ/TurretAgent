using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassicalEnemySpawner : MonoBehaviour {
    public bool CleanToWin = false;
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

    private float mCurrentTime;
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
        if (GameManager.sTheGlobalBehavior.mEnemyManager.IsEmpty())
        {
            mCurrentTime += Time.smoothDeltaTime*1f; // Speed 1x up
        }
        if (mCurrentSequenceIndex < mSequence.Count && mSequence[mCurrentSequenceIndex].time < mCurrentTime)
        {
            Spawn(mSequence[mCurrentSequenceIndex].code, mSequence[mCurrentSequenceIndex].pathName);
            mCurrentSequenceIndex++;
        }
        
        if (CleanToWin && mCurrentSequenceIndex == mSequence.Count && GameManager.sTheGlobalBehavior.mEnemyManager.IsEmpty())
        {
            GameManager.sTheGlobalBehavior.GameWin();
        }
    }
}