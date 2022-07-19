using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHotkeyBar : MonoBehaviour
{
    public void Activate(int index) {
        Transform border = transform.Find("slot"+index).Find("border");
        border.GetComponent<Image>().enabled = true;
    }
    public void Deactivate(int index) {
        Transform border = transform.Find("slot"+index).Find("border");
        border.GetComponent<Image>().enabled = false;
    }
    public void StartCooldDown(float time, int index)
    {

    }

}
