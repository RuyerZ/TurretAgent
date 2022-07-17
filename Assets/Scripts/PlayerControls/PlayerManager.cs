using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private bool isCarrying = false;
    private bool isRepairing = false;
    //to be updated
    private List<string> barItems = new List<string>() {
        "Pistol", "Rifle", "Empty", "Empty", "Empty"
    };
    private int activeIndex = 0;

    //set in editor
    public UIHotkeyBar bar;
    public PlayerController controller; 
    public Weapon weapon;

    void Awake()
    {
        bar.setPlayerManager(this);
        controller.setPlayerManager(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveItem(int index)
    {
        string Item = barItems[index];
        if (Item == "Empty") return;

        weapon.SetWeapon(barItems[index]);
        bar.SetActive(index);
        activeIndex = index;
    }
    public void Fire()
    {
        weapon.Fire();
    }
}
