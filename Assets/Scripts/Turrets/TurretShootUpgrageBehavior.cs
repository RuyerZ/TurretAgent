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
        float cost = 10f + 5f * level;
        Debug.Log(_Turret._BulletPre.dmg);
        Debug.Log(_Turret._AttackIntervalReset);
        Debug.Log(_Turret._AttackRadius);
        return new List<(string, float)>() {
            ("Damage: " + _Turret._BulletPre.dmg.ToString("N1"), cost),
            ("CD: " + _Turret._AttackIntervalReset.ToString("N2"), cost),
            ("Range: "+ _Turret._AttackRadius.ToString("N1"),cost),
        };
    }
    public override bool Upgrade(int index) {
        switch (index) {
            case 0:
                _Turret._BulletPre.dmg += 1;
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