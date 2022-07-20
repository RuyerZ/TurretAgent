using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour {
    public GameObject turret;
    public Text upgradesLeftText;
    public GameObject cover;
    private bool isEnabled = false;
    public AudioSource btnSound;

    public void Close() {
        gameObject.SetActive(false);
        GameManager.sTheGlobalBehavior.Resume();
    }
    void Start() {
    }
    void Update() {
        int upgradesLeft = GameManager.sTheGlobalBehavior.GetUpgradesLeft();
        
        if (upgradesLeft > 0) {
            cover.gameObject.SetActive(false);
            isEnabled = true;
        } else {
            cover.gameObject.SetActive(true);
            isEnabled = false;
        }
        upgradesLeftText.text = upgradesLeft.ToString();
    }
    public bool GenericBtn() {
        if (!isEnabled) return false;
        if (!GameManager.sTheGlobalBehavior.UseUpgrade()) return false;
        btnSound.Play();
        return true;
    }

    public void OnUpgradeRangeBtn() {
        if (!GenericBtn()) return;
        turret.GetComponent<TurretShootBehavior>()._AttackRadius += 0.5f;
    }
    public void OnUpgradeSpeedBtn() {
        if (!GenericBtn()) return;
        turret.GetComponent<TurretShootBehavior>()._AttackInterval *= 0.6f;
    }
    public void OnUpgradeDamageBtn() {
        if (!GenericBtn()) return;
        turret.GetComponent<TurretShootBehavior>()._BulletPre.dmg += 0.5f;
    }
}