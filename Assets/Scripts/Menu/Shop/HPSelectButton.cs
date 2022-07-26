using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSelectButton : ItemSelectButtonBase {
    public override bool InnerBuy() {
        GameManager.sTheGlobalBehavior.AddBaseHP(mItemCount);
        return true;
    }
}