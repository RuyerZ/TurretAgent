using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour {
    private TurretUpgradeBase turretUpgradeBehavior;
    private List<(string, string, string, float)> upgrades = null;
    public AudioSource btnSound;
    public int upgradeUICount = 4;

    public void Close() {
        gameObject.SetActive(false);
        GameManager.sTheGlobalBehavior.Resume("upgrade");
    }
    void OnDisable() {
        turretUpgradeBehavior = null;
        upgrades = null;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {
            CloseUI();
        }
        UpdateUI();
    }
    public void CloseUI() {
        GameManager.sTheGlobalBehavior.Resume("upgrade");
        gameObject.SetActive(false);
    }
    public void SetTurret(GameObject t) {
        Debug.Assert(t);
        turretUpgradeBehavior = t.GetComponent<TurretUpgradeBase>();
    }
    private void UpdateUI() {
        if (turretUpgradeBehavior == null) return;
        upgrades = turretUpgradeBehavior.GetUpgrades();

        for (int i = 0; i < upgradeUICount; i++) {
            Transform upgradeUI = transform.Find("upgrade"+i);
            if (i >= upgrades.Count) {
                upgradeUI.gameObject.SetActive(false);
            } else {
                upgradeUI.gameObject.SetActive(true);
                upgradeUI.Find("LevelText").GetComponentInChildren<Text>().text = upgrades[i].Item1;
                upgradeUI.Find("UpgradeText").GetComponent<Text>().text = upgrades[i].Item2;
                upgradeUI.Find("AfterUpgrade").GetComponentInChildren<Text>().text = upgrades[i].Item3;

                float cost = upgrades[i].Item4;
                if (cost < 0) {
                    upgradeUI.Find("UpgradeButton").GetComponentInChildren<Text>().text = "-";
                    upgradeUI.Find("UpgradeButton").GetComponent<Button>().interactable = false;
                }
                else {
                    upgradeUI.Find("UpgradeButton").GetComponentInChildren<Text>().text = "$" + upgrades[i].Item4.ToString("#.#");
                    upgradeUI.Find("UpgradeButton").GetComponent<Button>().interactable = true;
                }
            }
        }

        transform.Find("Title").GetComponent<Text>().text = turretUpgradeBehavior.GetTurretName();
    }
    public bool Upgrade(int index) {
        if (turretUpgradeBehavior == null) return false;
        if (!GameManager.sTheGlobalBehavior.Buy(upgrades[index].Item4)) return false;
        turretUpgradeBehavior.Upgrade(index);
        return true;
    }
    public void OnBtnClick(int index) {
        if (!Upgrade(index)) return;
        btnSound.Play();
    }
}