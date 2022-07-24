using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStatUI : MonoBehaviour
{
    public ClassicalEnemySpawner classicalSpawner = null;
    public EnemyHPBehavior bossHP = null;

    // Update is called once per frame
    void Update()
    {
        Text txt = GetComponentInChildren<Text>();
        string msg = "";

        if (GameManager.sTheGlobalBehavior.GetIsPrepare()) {
            msg = "Prepare for enemies!";
            txt.alignment = TextAnchor.UpperCenter;
            transform.Find("StartButton").gameObject.SetActive(true);
        }
        else if (classicalSpawner != null) {
            msg = "Defend your home!\nEnemy Left: ";
            msg += classicalSpawner.GetEnemiesLeftString();
            txt.alignment = TextAnchor.UpperLeft;
            transform.Find("StartButton").gameObject.SetActive(false);
        }
        else if (bossHP != null) {
            msg = "defeat the boss!\nBOSS HP: ";
            msg += bossHP.GetHPString();
            txt.alignment = TextAnchor.UpperLeft;
            transform.Find("StartButton").gameObject.SetActive(false);
        }

        txt.text = msg;
    }
    void onStartBtnClick() {
        GameManager.sTheGlobalBehavior.StartWave();
    }
}
