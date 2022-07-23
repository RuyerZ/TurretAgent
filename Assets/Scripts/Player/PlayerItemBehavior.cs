using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemBehavior : MonoBehaviour
{
    public UIHotkeyBar bar;
    private bool activated = true;
    
    [SerializeField]
    private List<ItemBase> barItems = new List<ItemBase>() {
        null, null, null, null, null
    };
    private int activeIndex = 0;

    void Awake()
    {
        SetActiveItem(activeIndex);
        for (int i = 0; i < barItems.Count; i++) {
            SetItemIconUI(i);
            SetItemCountUI(i);
        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < barItems.Count; i++) {
            SetItemCountUI(i);
        }
        if (activated) handleInput();
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
    public void SetItem(ItemBase item, int index)
    {
        barItems[index] = item;
    }
    public void Activate()
    {
        barItems[activeIndex].Activate();
        activated = true;
    }
    public void Deactivate()
    {
        barItems[activeIndex].Deactivate();
        activated = false;
    }
    private void SetItemIconUI(int index)
    {
        ItemBase item = barItems[index];
        if (item != null) {
            bar.SetItemIcon(index, item.getIcon());
        } else {
            bar.SetItemIcon(index, null);
        }
        
    }
    private void SetItemCountUI(int index)
    {
        ItemBase item = barItems[index];
        string c = "";

        if (item != null) {
            int count = item.getItemCount();
            if (count >= 0) c = count.ToString();
            else c = "∞";
        } 
        bar.SetItemCount(index, c);
    }
    public bool AddItem(string itemname, int count) {
        ItemBase[] items = GetComponentsInChildren<ItemBase>(true);
        foreach (ItemBase item in barItems) {
            if (item == null) continue;
            if (item.name == itemname) {
                // if item exists and is infinite, do nothing
                if (count == -1 || item.getItemCount() < 0) {
                    return false;
                }
                // item exists and stackable, add count
                item.setItemCount(item.getItemCount() + count);
                return true;
            }
        }
        ItemBase item_to_add = null;
        foreach (ItemBase item in items) {
            if (item.name == itemname) {
                item_to_add = item;
                break;
            }
        }
        if (item_to_add == null) {
            Debug.Log("item name not found!");
            return false;
        }
        for (int i=0; i<barItems.Count; ++i) {
            if (barItems[i] == null) {
                barItems[i] = item_to_add;
                SetItemIconUI(i);
                SetItemCountUI(i);
                return true;
            }
        }
        return false;
    }
    public bool RemoveItem(string itemname,int count) {
        for (int i=0; i< barItems.Count;++i) {
            if (barItems[i] == null) continue;
            if (barItems[i].name == itemname) {
                if (barItems[i].getItemCount()<0  || barItems[i].getItemCount() <= count) {
                    if (activeIndex == i && activated) {
                        Deactivate();
                    }
                    barItems[i] = null;
                    SetItemIconUI(i);
                    SetItemCountUI(i);
                    return true;
                }
                barItems[i].setItemCount(barItems[i].getItemCount() - count);
                SetItemCountUI(i);
                return true;
            }
        }
        return false;
    }
}
