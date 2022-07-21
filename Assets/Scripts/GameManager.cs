using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager sTheGlobalBehavior = null; // Single pattern

    public PlayerMoveBehavior mHero = null;  // must set in the editor
    public PathSystem mPathSystem = null;
    public EnemyManager mEnemyManager = new EnemyManager();
    public FriendManager mFriendManager = new FriendManager();
    public float mMaxBaseHP = 10f;
    public HPBar mHPBar = null;
    public GameObject WinUI = null;
    public GameObject LoseUI = null;
    public GameObject UpgradeUI = null;
    private float mBaseHP;
    public bool isPaused = false;

    private float Gold = 9990;

    // Start is called before the first frame update
    void Awake() {
        GameManager.sTheGlobalBehavior = this;  // Singleton pattern
        Time.timeScale = 1;
        //Debug.Assert(mHero != null);
        //Debug.Assert(mEnemyManager != null);
        //Debug.Assert(mPathSystem != null);
    }
    void Start() {
        mBaseHP = mMaxBaseHP;
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) Resume();
            else Pause();
        }
    }
    private bool isGameEnd = false;
    public void GameFail() {
        if (!isGameEnd) {
            isGameEnd = true;
            Pause();
            LoseUI.SetActive(true);
        }
    }
    public void GameWin() {
        if (!isGameEnd) {
            isGameEnd = true;
            Pause();
            WinUI.SetActive(true);
        }
    }
    public void ReduceBaseHP(float dmg) {
        mBaseHP -= dmg;
        if (mBaseHP <= 0) {
            GameFail();
        }
        mHPBar.Set(mBaseHP/mMaxBaseHP);
    }
    public void Pause() {
        isPaused = true;
        Time.timeScale = 0;
        mHero.gameObject.SetActive(false);
        mPathSystem.gameObject.SetActive(false);
    }
    public void Resume() {
        isPaused = false;
        Time.timeScale = 1;
        mHero.gameObject.SetActive(true);
        mPathSystem.gameObject.SetActive(true);
    }
    public void AddGold(float gold) 
    {
        Gold += gold;
    }
    public void SetGold(float gold) 
    {
        Gold = gold;
    }
    public float GetGold() {
        return Gold;
    }
    public bool Buy(float gold) {
        if (Gold >= gold) {
            Gold -= gold;
            return true;
        }
        return false;
    }
}