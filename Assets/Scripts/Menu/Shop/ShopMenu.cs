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
    private float gold = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        buyButton.interactable = false;

        itemNumber = itemWindows.Length;
        for (int i = 0; i < itemNumber; i++)
            itemWindows[i].button.Index = i;

        if (GameManager.sTheGlobalBehavior != null)
            gold = GameManager.sTheGlobalBehavior.GetGold();


        goldText.text = "GOLD: " + gold.ToString() + " $";
    }

    // Update is called once per frame
    void Update()
    {
        buyButton.interactable = IsItemAvailable();

        goldText.text = "GOLD: " + gold.ToString() + " $";
    }

    private bool IsItemAvailable()
    {
        return (itemIndex >= 0)
            && (!itemWindows[itemIndex].button.IsSold)
            && (itemPrice <= gold);
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
            itemWindows[itemIndex].button.SetSold();

            gold -= itemPrice;
            GameManager.sTheGlobalBehavior.SetGold(gold);
        }
    }

    public void Continue()
    {
        gameObject.SetActive(false);
    }
}
