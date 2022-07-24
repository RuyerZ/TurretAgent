using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootUpgrageBehavior : TurretUpgradeBase {
    TurretShootBehavior _Turret;
    int levelDamage = 0;
    int levelCD = 0;
    int levelRange = 0;
    void Start() {
        _Turret = GetComponent<TurretShootBehavior>();
        Debug.Assert(_Turret != null);
    }
    public override List<(string, float)> GetUpgrades() {
        float costDamage = 5f + 5f * levelDamage;
        float costCD = 5f + 5f * levelCD;
        float costRange = 5f + 5f * levelRange;
        return new List<(string, float)>() {
            ("Damage: " + _Turret._AttackDamage.ToString("N1"), costDamage),
            ("CD: " + _Turret._AttackInterval.ToString("N2"), costCD),
            ("Range: "+ _Turret._AttackRadius.ToString("N1"),costRange),
        };
    }
    public override bool Upgrade(int index) {
        switch (index) {
            case 0:
                _Turret._AttackDamage += 1;
                levelDamage++;
                break;
            case 1:
                _Turret._AttackInterval *= 0.8f;
                if (_Turret._AttackInterval < 0.01f) _Turret._AttackInterval = 0.01f;
                levelCD++;
                break;
            case 2:
                _Turret._AttackRadius += 1;
                levelRange++;
                break;
            default:
                return false;
        }
        return true;
    }
}