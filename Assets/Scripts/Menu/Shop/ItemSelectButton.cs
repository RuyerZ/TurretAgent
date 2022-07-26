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
    public bool isTurret = false;
    public GameObject Prefab;
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

        mText = transform.Find("Label").gameObject.GetComponentInChildren<Text>();
        if (mItemName.Length == 0) {
            SetWindowActive(false);
            mButton.interactable = false;
            mSold = true;
        }
        mText.text = mItemName;
        if (mItemCount > 0) {
            mText.text += " x" + mItemCount;
        }
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
    public bool InnerBuy() {
        if (!isTurret) {
            return GameManager.sTheGlobalBehavior.mHero.gameObject.GetComponent<PlayerItemBehavior>().AddItem(mItemName, mItemCount);
        } else {
            Vector3 pos = GameManager.sTheGlobalBehavior.mHero.transform.position;
            Quaternion rot = GameManager.sTheGlobalBehavior.mHero.transform.rotation;
            Instantiate(Prefab, pos, rot);
            return true;
        }
    }
    public bool SetSold()
    {
        bool success = InnerBuy();
        if (!success) return false;
        if (!isInfinity) {
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
