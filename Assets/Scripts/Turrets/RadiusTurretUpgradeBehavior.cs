using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusTurretUpgradeBehavior : TurretUpgradeBase {
    RadiusTurretBehavior _Turret;
    int level = 0;
    void Start() {
        _Turret = GetComponent<RadiusTurretBehavior>();
        Debug.Assert(_Turret != null);
    }
    public override List<(string, float)> GetUpgrades() {
        float cost = 30f + 10f * level;
        return new List<(string, float)>() {
            ("Damage: " + _Turret._AttackDamage.ToString("N1"), cost),
            ("CD: " + _Turret._AttackIntervalReset.ToString("N2"), cost),
            ("Range: "+ _Turret._AttackRadius.ToString("N1"),cost),
        };
    }
    public override bool Upgrade(int index) {
        switch (index) {
            case 0:
                _Turret._AttackDamage += 0.5f;
                break;
            case 1:
                _Turret._AttackIntervalReset *= 0.8f;
                if (_Turret._AttackIntervalReset < 0.01f) _Turret._AttackIntervalReset = 0.01f;
                break;
            case 2:
                _Turret._AttackRadius += 1;
                break;
            default:
                return false;
        }
        level++;
        return true;
    }
}