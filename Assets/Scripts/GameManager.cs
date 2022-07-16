using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static GameManager sTheGlobalBehavior = null; // Single pattern

    public PlayerController mHero = null;  // must set in the editor

    // Start is called before the first frame update
    void Start() {
        GameManager.sTheGlobalBehavior = this;  // Singleton pattern
        Debug.Assert(mHero != null);

    }

    // Update is called once per frame
    void Update() {
    }
}