using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [System.Serializable]
    public struct ItemWindow
    {
        public ItemSelectButton button;
        public GameObject description;
    }

    public ItemWindow[] itemWindows;
    public Button buyButton;
    public Text goldText;
    private int itemNumber;
    private int itemIndex = -1;
    private float itemPrice = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        buyButton.interactable = false;

        itemNumber = itemWindows.Length;
        for (int i = 0; i < itemNumber; i++)
            itemWindows[i].button.Index = i;

        goldText.text = "GOLD: " + GameManager.sTheGlobalBehavior.Gold.ToString() + " $";
    }

    // Update is called once per frame
    void Update()
    {
        buyButton.interactable = IsItemAvailable();

        goldText.text = "GOLD: " + GameManager.sTheGlobalBehavior.Gold.ToString() + " $";
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
        gameObject.SetActive(false);
    }
}
