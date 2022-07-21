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
    private int itemNumber;
    private int itemIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        buyButton.interactable = false;

        itemNumber = itemWindows.Length;
        for (int i = 0; i < itemNumber; i++)
            itemWindows[i].button.Index = i;
    }

    // Update is called once per frame
    void Update()
    {
        buyButton.interactable = (itemIndex >= 0) && !itemWindows[itemIndex].button.IsSold;
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
    }

    public void Buy()
    {
        if (itemIndex >= 0 && !itemWindows[itemIndex].button.IsSold)
        {
            Debug.Log("Buy Item " + itemIndex.ToString());
            itemWindows[itemIndex].button.SetSold();
        }
    }

    public void Continue()
    {
        gameObject.SetActive(false);
    }
}
