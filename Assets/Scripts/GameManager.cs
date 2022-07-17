using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager sTheGlobalBehavior = null; // Single pattern

    public PlayerController mHero = null;  // must set in the editor
    public PathSystem mPathSystem = null;
    public EnemyManager mEnemyManager = null;
    

    // Start is called before the first frame update
    void Awake() {
        GameManager.sTheGlobalBehavior = this;  // Singleton pattern
        mEnemyManager = new EnemyManager();
        //Debug.Assert(mHero != null);
        //Debug.Assert(mEnemyManager != null);
        //Debug.Assert(mPathSystem != null);
    }

    // Update is called once per frame
    void Update() {
    }
}