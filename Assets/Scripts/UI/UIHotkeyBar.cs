using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHotkeyBar : MonoBehaviour
{
    private int activeIndex = 0;
    static private PlayerManager manager= null;
    public void setPlayerManager(PlayerManager m) {
        manager = m;
    }

    void Awake()
    {
        SetActive(activeIndex);
    }

    public void SetActive(int index) {
        Transform border = transform.Find("slot"+activeIndex).Find("border");
        border.GetComponent<Image>().enabled = false;
        Transform border1 = transform.Find("slot"+index).Find("border");
        border1.GetComponent<Image>().enabled = true;
        activeIndex = index;
    }

    // Update is called once per frame
    void Update()
    {
    }

}
