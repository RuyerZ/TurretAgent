using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectButton : MonoBehaviour
{
    public GameObject mSelectedEffect;
    private int mItemIndex;
    private ShopMenu mShopMenu;
    // Start is called before the first frame update
    void Start()
    {
        mShopMenu = GetComponentInParent<ShopMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Index { get { return mItemIndex; } set { mItemIndex = value; } }

    public void SetWindowActive(bool active)
    {
        mSelectedEffect.SetActive(active);
    }

    public void OnClick()
    {
        if (mShopMenu != null)
            mShopMenu.Select(mItemIndex);
    }
}
