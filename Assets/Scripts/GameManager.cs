using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager sTheGlobalBehavior = null; // Single pattern

    public PlayerMoveBehavior mHero = null;  // must set in the editor
    public PathSystem mPathSystem = null;
    public EnemyManager mEnemyManager = null;
    
    public float mBaseHP = 100f;

    // Start is called before the first frame update
    void Awake() {
        GameManager.sTheGlobalBehavior = this;  // Singleton pattern
        //Debug.Assert(mHero != null);
        //Debug.Assert(mEnemyManager != null);
        //Debug.Assert(mPathSystem != null);
    }

    // Update is called once per frame
    void Update() {
    }
    public void GameFail() {
        Debug.Log("GameFail");
    }
    public void GameWin() {
        //Debug.Log("GameWin");
    }
    public void ReduceBaseHP(float dmg) {
        mBaseHP -= dmg;
        if (mBaseHP <= 0) {
            GameFail();
        }
    }
}