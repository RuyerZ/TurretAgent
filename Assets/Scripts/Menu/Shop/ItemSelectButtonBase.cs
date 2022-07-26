using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemSelectButtonBase : MonoBehaviour {
    public GameObject mSelectedEffect;
    public string mItemName;
    public int mItemCount;
    public float mPrice = 1.0f;
    public int MaxBuyCount = -1;
    private int LeftBuyCount;
    private int mItemIndex;
    private bool mSold = false;
    private ShopMenu mShopMenu;
    private Button mButton;
    private Text mText;
    public int Index { get { return mItemIndex; } set { mItemIndex = value; } }
    public bool IsSold { get { return mSold; } }
    public float Price { get { return mPrice; } }
    public void SetWindowActive(bool active)
    {
        mSelectedEffect.SetActive(active);
    }
    public abstract bool InnerBuy();
    void Start()
    {
        mShopMenu = GetComponentInParent<ShopMenu>();
        mButton = GetComponentInChildren<Button>();

        mText = transform.Find("Label").gameObject.GetComponentInChildren<Text>();
        if (mItemName.Length == 0) {
            SetWindowActive(false);
            mButton.interactable = false;
            mSold = true;
        }
        mText.text = mItemName;
        if (mItemCount > 1) {
            mText.text += " x" + mItemCount;
        }
        mText.text += " ";
        mText.text += mPrice.ToString() + " $";
        LeftBuyCount = MaxBuyCount;
    }
    public bool SetSold()
    {
        bool success = InnerBuy();
        if (!success) return false;
        if (LeftBuyCount>0) LeftBuyCount--;
        if (LeftBuyCount == 0) {
            SetWindowActive(false);
            mButton.interactable = false;
            transform.Find("greyedOut").gameObject.SetActive(true);
            mSold = true;
        }
        return true;
    }
    public void OnClick()
    {
        if (!mSold)
            mShopMenu.Select(mItemIndex);
    }
}