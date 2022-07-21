using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour {
    private TurretShootUpgrageBehavior turretUpgradeBehavior;
    private List<(string, float)> upgrades = null;
    public AudioSource btnSound;

    public void Close() {
        gameObject.SetActive(false);
        GameManager.sTheGlobalBehavior.Resume();
    }
    void OnDisable() {
        turretUpgradeBehavior = null;
        upgrades = null;
    }
    void Update() {

    }
    public void SetTurret(GameObject t) {
        Debug.Assert(t);
        turretUpgradeBehavior = t.GetComponent<TurretShootUpgrageBehavior>();
        upgrades = turretUpgradeBehavior.GetUpgrades();

        for (int i = 0; i < upgrades.Count; i++) {
            Transform upgradeUI = transform.Find("upgrade"+i);
            upgradeUI.Find("UpgradeText").GetComponent<Text>().text = upgrades[i].Item1;
            upgradeUI.Find("UpgradeButton").GetComponentInChildren<Text>().text = "$" + upgrades[i].Item2.ToString("D");
        }

        transform.Find("Title").GetComponent<Text>().text = turretUpgradeBehavior.GetTurretName();

    }
    public bool Upgrade(int index) {
        if (turretUpgradeBehavior == null) return false;
        if (!GameManager.sTheGlobalBehavior.Buy(upgrades[index].Item2)) return false;
        turretUpgradeBehavior.Upgrade(index);
        return true;
    }
    public void OnBtnClick(int index) {
        if (!Upgrade(index)) return;
        btnSound.Play();
    }
}