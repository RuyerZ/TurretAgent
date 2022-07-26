using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretUpgradeBase : MonoBehaviour {
    public string turretName = "Basic Tower 1";

    public string GetTurretName() {
        return turretName;
    }
    public virtual List<(string,string,string,float)> GetUpgrades() {
        return new List<(string,string,string,float)>();
    }
    public virtual bool Upgrade(int index) {
        return false;
    }
}