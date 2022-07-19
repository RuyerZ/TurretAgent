using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour {
    public GameObject turret;
    public void Close() {
        gameObject.SetActive(false);
        GameManager.sTheGlobalBehavior.Resume();
    }
    void Start() {
        GameManager.sTheGlobalBehavior.Pause();
        Debug.Log("Well");
    }
    public void OnUpgradeRangeBtn() {
        turret.GetComponent<TurretShootBehavior>()._AttackRadius += 0.5f;
        Close();
    }
    public void OnUpgradeSpeedBtn() {
        turret.GetComponent<TurretShootBehavior>()._AttackInterval *= 0.8f;
        Close();
    }
    public void OnUpgradeDamageBtn() {
        turret.GetComponent<TurretShootBehavior>()._BulletPre.dmg += 0.5f;
        Close();
    }
}