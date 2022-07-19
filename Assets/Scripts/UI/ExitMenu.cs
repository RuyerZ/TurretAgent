using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitMenu : MonoBehaviour {
    void Start() {
        GameManager.sTheGlobalBehavior.Pause();
    }
    public void onMenuBtn() {
        
        SceneManager.LoadScene("MenuScene");
    }
}