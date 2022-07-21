using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretUpgradeBase : MonoBehaviour {
    
    public virtual List<(string,float)> GetUpgrades() {
        return new List<(string,float)>();
    }
    public virtual bool Upgrade(int index) {
        return false;
    }
}