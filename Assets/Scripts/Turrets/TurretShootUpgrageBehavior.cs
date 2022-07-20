using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootUpgrageBehavior : TurretUpgradeBase {
    TurretShootBehavior _Turret;
    int level = 0;
    void Start() {
        _Turret = GetComponent<TurretShootBehavior>();
        Debug.Assert(_Turret != null);
    }
    public override List<(string, float)> GetUpgrades() {
        float cost = 100f + 50f * level;
        return new List<(string, float)>() {
            ("Damage", cost),
            ("Cooldown", cost),
            ("Range", cost),
        };
    }
    public override bool Upgrade(int index) {
        switch (index) {
            case 0:
                _Turret._BulletPre.dmg += 1;
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