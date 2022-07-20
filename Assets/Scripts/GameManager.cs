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
    private bool isPaused = false;

    //temp
    private int XP = 0;
    private int turretLevel = 1;
    private int upgradesLeft = 0;
    private int XPToLevelUp = 5;

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
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) Resume();
            else Pause();
        }
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
            WinUI.SetActive(true);
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
        isPaused = true;
        Time.timeScale = 0;
        mHero.gameObject.SetActive(false);
        mPathSystem.gameObject.SetActive(false);
        mEnemyManager.gameObject.SetActive(false);
    }
    public void Resume() {
        isPaused = false;
        Time.timeScale = 1;
        mHero.gameObject.SetActive(true);
        mPathSystem.gameObject.SetActive(true);
        mEnemyManager.gameObject.SetActive(true);
    }
    public void AddXP(int xp)
    {
        for (int i = 0; i < xp; i++)
        {
            XP++;
            if (XP == XPToLevelUp) {
                XP = 0;
                levelUp();
            }
        }
    }
    private void levelUp() {
        upgradesLeft++;
        turretLevel++;
    }
    public int GetXP() {
        return XP;
    }
    public int GetXPToLevelUp() {
        return XPToLevelUp;
    }
    public int GetUpgradesLeft() {
        return upgradesLeft;
    }
    public bool UseUpgrade() {
        if (upgradesLeft> 0) {
            upgradesLeft--;
            return true;
        }
        return false;
    }
}