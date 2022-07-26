using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusTurretUpgradeBehavior : TurretUpgradeBase {
    RadiusTurretBehavior _Turret;
    TurretHPBehavior _HP;
    int[] currentLevels = {0,0,0,0};
    int[] maxLevels = {10,5,4,10};
    float[] initCosts = {10,10,10,5};
    float[] costFactors = {5,10,10,5};
    string[] upgradeNames = {"Damage", "Cooldown", "Range", "Health"};

    void Start() {
        _Turret = GetComponent<RadiusTurretBehavior>();
        _HP = GetComponent<TurretHPBehavior>();
        Debug.Assert(_Turret != null);
        Debug.Assert(_HP != null);
    }
    public override List<(string, string, string, float)> GetUpgrades() {
        List<(string, string, string, float)> l = new List<(string, string, string, float)>();
        for (int i = 0; i < 4; i++) {
            string lvlString = "LVL " + currentLevels[i].ToString() + "/" + maxLevels[i].ToString();
            string upgradeText = upgradeNames[i] + ": " + getCurrentStat(i).ToString("N2");
            string nextUpgrade = getNextUpgradeString(i);
            float cost = initCosts[i] + currentLevels[i] * costFactors[i];

            l.Add((lvlString,upgradeText,nextUpgrade,cost));
        }
        return l;
    }
    public override bool Upgrade(int index) {
        if (currentLevels[index] >= maxLevels[index]) return false;
        switch (index) {
            case 0:
                _Turret._AttackDamage = getNextUpgrade(index);
                break;
            case 1:
                _Turret._CoolingTimelReset = getNextUpgrade(index);
                if (_Turret._CoolingTimelReset < 0.01f) _Turret._CoolingTimelReset = 0.01f;
                break;
            case 2:
                _Turret._AttackRadius = getNextUpgrade(index);
                break;
            case 3:
                _HP.maxHP = getNextUpgrade(index);
                break;
            default:
                return false;
        }
        currentLevels[index]++;
        return true;
    }
    float getNextUpgrade(int index) {
        if (currentLevels[index] >= maxLevels[index]) return -999;

        float next;
        switch (index) {
            case 0:
                next = _Turret._AttackDamage * 1.25f;
                break;
            case 1:
                next = _Turret._CoolingTimelReset * 0.8f;
                break;
            case 2:
                next = _Turret._AttackRadius + 0.3f;
                break;
            case 3:
                next = _HP.maxHP + 5;
                break;
            default:
                return -9999;
        }
        return next;
    }
    string getNextUpgradeString(int index) {
        string nextStr = "";
        float next = getNextUpgrade(index);
        if (next == -999) nextStr = "MAX";
        else {
            nextStr = next.ToString("N2");
        }
        return nextStr;
    }
    float getCurrentStat(int index) {
        float stat;
        switch (index) {
            case 0:
                stat = _Turret._AttackDamage;
                break;
            case 1:
                stat = _Turret._CoolingTimelReset;
                break;
            case 2:
                stat = _Turret._AttackRadius;
                break;
            case 3:
                stat = _HP.maxHP;
                break;
            default:
                stat = -9999;
                break;
        }
        return stat;
    }
}