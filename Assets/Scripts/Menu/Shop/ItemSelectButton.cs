using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectButton : MonoBehaviour
{
    public GameObject mSelectedEffect;
    private int mItemIndex;
    private bool mSold = false;
    private ShopMenu mShopMenu;
    private Button mButton;
    // Start is called before the first frame update
    void Start()
    {
        mShopMenu = GetComponentInParent<ShopMenu>();
        mButton = GetComponentInChildren<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Index { get { return mItemIndex; } set { mItemIndex = value; } }
    public bool IsSold { get { return mSold; } }

    public void SetWindowActive(bool active)
    {
        mSelectedEffect.SetActive(active);
    }

    public void SetSold()
    {
        SetWindowActive(false);
        mButton.interactable = false;
        mSold = true;
    }

    public void OnClick()
    {
        if (!mSold)
            mShopMenu.Select(mItemIndex);
    }
}
