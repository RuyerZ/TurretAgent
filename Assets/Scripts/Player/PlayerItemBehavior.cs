using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemBehavior : MonoBehaviour
{
    public UIHotkeyBar bar;
    
    [SerializeField]
    private List<ItemBase> barItems = new List<ItemBase>() {
        null, null, null, null, null
    };
    private int activeIndex = 0;

    void Awake()
    {
        SetActiveItem(activeIndex);
        for (int i = 0; i < barItems.Count; i++) {
            if (barItems[i] != null) {
                bar.SetItemIcon(i, barItems[i].getIcon());
            } else {
                bar.SetItemIcon(i, null);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        handleInput();
    }

    private void handleInput()
    {
        if (Input.GetMouseButton(0))
        {
            barItems[activeIndex].Fire();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetActiveItem(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetActiveItem(4);
        }
    }

    private void SetActiveItem(int i)
    {
        if (barItems[i] == null) return;

        if (!barItems[activeIndex].Deactivate()) {
            Debug.Log("deactivate error!");
            return;
        }
        bar.Deactivate(activeIndex);

        if (!barItems[i].Activate()) {
            Debug.Log("activate error!");
            return;
        }
        bar.Activate(i);
        
        activeIndex = i;
    }
    private void SetItem(ItemBase item, int index)
    {
        barItems[index] = item;
    }
    public void Activate()
    {
        barItems[activeIndex].Activate();
    }
    public void Deactivate()
    {
        barItems[activeIndex].Deactivate();
    }
    
}
