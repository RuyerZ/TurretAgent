using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager sTheGlobalBehavior = null; // Single pattern

    public PlayerMoveBehavior mHero = null;  // must set in the editor
    public PathSystem mPathSystem = null;
    public EnemyManager mEnemyManager = null;
    public float mMaxBaseHP = 10f;
    public HPBar mHPBar = null;
    public GameObject WinUI = null;
    public GameObject LoseUI = null;
    private float mBaseHP;

    // Start is called before the first frame update
    void Awake() {
        GameManager.sTheGlobalBehavior = this;  // Singleton pattern
        //Debug.Assert(mHero != null);
        //Debug.Assert(mEnemyManager != null);
        //Debug.Assert(mPathSystem != null);
    }
    void Start() {
        mBaseHP = mMaxBaseHP;
    }
    // Update is called once per frame
    void Update() {
    }
    private bool isGameEnd = false;
    public void GameFail() {
        if (!isGameEnd) {
            isGameEnd = true;
            LoseUI.SetActive(true);
        }
    }
    public void GameWin() {
        if (!isGameEnd) {
            isGameEnd = true;
            LoseUI.SetActive(true);
        }
    }
    public void ReduceBaseHP(float dmg) {
        Debug.Log("Base Hurt");
        mBaseHP -= dmg;
        if (mBaseHP <= 0) {
            GameFail();
        }
        mHPBar.Set(mBaseHP/mMaxBaseHP);
    }
    public void Pause() {
        Time.timeScale = 0;
        mHero.gameObject.SetActive(false);
    }
    public void Resume() {
        Time.timeScale = 1;
        mHero.gameObject.SetActive(true);
    }
}