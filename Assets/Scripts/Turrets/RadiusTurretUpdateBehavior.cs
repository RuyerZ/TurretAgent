using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusTurretUpgrageBehavior : TurretUpgradeBase {
    RadiusTurretBehavior _Turret;
    int level = 0;
    void Start() {
        _Turret = GetComponent<RadiusTurretBehavior>();
        Debug.Assert(_Turret != null);
    }
    public override List<(string, float)> GetUpgrades() {
        float cost = 30f + 10f * level;
        return new List<(string, float)>() {
            ("Damage", cost),
            ("Cooldown", cost),
            ("Range", cost),
        };
    }
    public override bool Upgrade(int index) {
        switch (index) {
            case 0:
                _Turret._AttackDamage += 0.5f;
                break;
            case 1:
                _Turret._AttackInterval *= 0.8f;
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