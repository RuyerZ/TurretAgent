using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectButton : ItemSelectButtonBase
{
    public override bool InnerBuy() {
        return GameManager.sTheGlobalBehavior.mHero.gameObject.GetComponent<PlayerItemBehavior>().AddItem(mItemName, mItemCount);
    }
}
