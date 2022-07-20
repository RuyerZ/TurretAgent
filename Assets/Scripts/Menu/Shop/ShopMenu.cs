using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public ItemSelectButton[] itemWindows;
    private int itemNumber;
    private int itemIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        itemNumber = itemWindows.Length;
        for (int i = 0; i < itemNumber; i++)
            itemWindows[i].Index = i;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select(int index)
    {
        Debug.Assert(index >= 0 && index < itemNumber);
        if (itemIndex >= 0)
            itemWindows[itemIndex].SetWindowActive(false);

        itemWindows[index].SetWindowActive(true);
        itemIndex = index;
    }
}
