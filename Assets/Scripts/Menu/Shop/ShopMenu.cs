using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [System.Serializable]
    public struct ItemWindow
    {
        public ItemSelectButtonBase button;
        public GameObject description;
    }

    public ItemWindow[] itemWindows;
    public Button buyButton;
    public Text goldText;
    private int itemNumber;
    private int itemIndex = -1;
    private float itemPrice = 0.0f;
    private bool isOpen = true;
    private GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.Find("Canvas").gameObject;
        GameManager.sTheGlobalBehavior.Pause("shop");
        buyButton.interactable = false;

        itemNumber = itemWindows.Length;
        for (int i = 0; i < itemNumber; i++)
            itemWindows[i].button.Index = i;

        goldText.text = "GOLD: " + GameManager.sTheGlobalBehavior.Gold.ToString() + " $";
    }
    void OpenShop()
    {
        isOpen = true;
        GameManager.sTheGlobalBehavior.Pause("shop");
        canvas.SetActive(true);
    }
    void CloseShop()
    {
        isOpen = false;
        GameManager.sTheGlobalBehavior.Resume("shop");
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen) 
        {
            buyButton.interactable = IsItemAvailable();

            goldText.text = "GOLD: " + GameManager.sTheGlobalBehavior.Gold.ToString() + " $";
        }
        if (Input.GetKeyDown(KeyCode.B) && 
            (!GameManager.sTheGlobalBehavior.isPaused || GameManager.sTheGlobalBehavior.GetPausedReason() == "shop")
            )
        {
            if (isOpen) CloseShop();
            else OpenShop();
        }
    }

    private bool IsItemAvailable()
    {
        return (itemIndex >= 0)
            && (!itemWindows[itemIndex].button.IsSold)
            && (itemPrice <= GameManager.sTheGlobalBehavior.Gold);
    }

    public void Select(int index)
    {
        Debug.Assert(index >= 0 && index < itemNumber);
        if (itemIndex >= 0)
        {
            itemWindows[itemIndex].button.SetWindowActive(false);
            itemWindows[itemIndex].description.SetActive(false);
        }

        itemWindows[index].button.SetWindowActive(true);
        itemWindows[index].description.SetActive(true);
        itemIndex = index;
        itemPrice = itemWindows[index].button.Price;
    }

    public void Buy()
    {
        if (IsItemAvailable())
        {
            Debug.Log("Buy Item " + itemIndex.ToString());
            if (itemWindows[itemIndex].button.SetSold()) {
                GameManager.sTheGlobalBehavior.Gold -= itemPrice;
            }
        }
    }

    public void Continue()
    {
        CloseShop();
    }

    public void onShopBtn() {
        if ((!GameManager.sTheGlobalBehavior.isPaused || GameManager.sTheGlobalBehavior.GetPausedReason() == "shop")
            )
        {
            if (isOpen) CloseShop();
            else OpenShop();
        }
    }
}
