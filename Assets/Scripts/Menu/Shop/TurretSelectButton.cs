using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretSelectButton : ItemSelectButtonBase
{
    public GameObject TurretPrefab;
    public override bool InnerBuy() {
        Vector3 pos = GameManager.sTheGlobalBehavior.mHero.transform.position;
        Quaternion rot = GameManager.sTheGlobalBehavior.mHero.transform.rotation;
        Instantiate(TurretPrefab, pos, rot);
        return true;
    }
}