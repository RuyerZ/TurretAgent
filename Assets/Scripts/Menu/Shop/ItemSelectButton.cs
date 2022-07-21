using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectButton : MonoBehaviour
{
    public GameObject mSelectedEffect;
    public float mPrice = 1.0f;
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
        mText.text = mPrice.ToString() + " $";
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
