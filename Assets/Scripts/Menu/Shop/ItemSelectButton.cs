using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectButton : MonoBehaviour
{
    public GameObject mSelectedEffect;
    public string mItemName;
    public int mItemCount;
    public float mPrice = 1.0f;
    public bool isInfinity = false;
    private int mItemIndex;
    private bool mSold = false;
    private ShopMenu mShopMenu;
    private Button mButton;
    private Text mText;
    // Start is called before the first frame update
    void Start()
    {
        mShopMenu = GetComponentInParent<ShopMenu>();
        mButton = GetComponentInChildren<Button>();

        mText = mButton.GetComponentInChildren<Text>();
        mText.text = mItemName;
        if (mItemCount > 0) {
            mText.text += " x" + mItemCount;
        }
        mText.text += " ";
        mText.text += mPrice.ToString() + " $";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Index { get { return mItemIndex; } set { mItemIndex = value; } }
    public bool IsSold { get { return mSold; } }
    public float Price { get { return mPrice; } }

    public void SetWindowActive(bool active)
    {
        mSelectedEffect.SetActive(active);
    }

    public bool SetSold()
    {
        bool success = GameManager.sTheGlobalBehavior.mHero.gameObject.GetComponent<PlayerItemBehavior>().AddItem(mItemName, mItemCount);
        if (!success) return false;
        if (!isInfinity) {
            SetWindowActive(false);
            mButton.interactable = false;
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
