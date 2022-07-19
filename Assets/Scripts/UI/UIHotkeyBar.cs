using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHotkeyBar : MonoBehaviour
{
    public Sprite emptySprite;

    public void SetItemIcon(int index, Sprite icon)
    {
        if (icon == null) icon = emptySprite;
        transform.Find("slot"+index).Find("Icon").GetComponent<Image>().sprite = icon;
    }
    public void SetItemCount(int index, int count)
    {

    }
    public void Activate(int index) 
    {
        Transform border = transform.Find("slot"+index).Find("border");
        border.GetComponent<Image>().enabled = true;
    }
    public void Deactivate(int index) 
    {
        Transform border = transform.Find("slot"+index).Find("border");
        border.GetComponent<Image>().enabled = false;
    }
    //TO BE IMPLEMENTED
    public void StartCooldDown(float time, int index)
    {

    }

}
