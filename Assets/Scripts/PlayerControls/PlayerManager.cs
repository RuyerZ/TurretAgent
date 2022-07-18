using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private bool isCarrying = false;
    private bool isRepairing = false;
    private float moveSpeed = 5f;
    //to be updated
    private List<string> barItems = new List<string>() {
        "Pistol", "Rifle", "Empty", "Empty", "Empty"
    };
    private int activeIndex = 0;

    //set in editor
    public UIHotkeyBar bar;
    public PlayerController controller; 
    public Weapon weapon;
    public TurretCarried turret;

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
        if (isCarrying || isRepairing) return;
        weapon.Fire();
    }
    public void ToggleIsCarry()
    {
        Vector3 toTurret = turret.transform.position - controller.transform.position;
        float distSqr = toTurret.sqrMagnitude;

        if (distSqr > 1f) {
            Debug.Log("too far");
            return;
        }

        if (isCarrying) {
            weapon.SetShowWeapon(true);
            turret.SetIsCarried(false);
        }
        else {
            weapon.SetShowWeapon(false);
            turret.SetIsCarried(true);
        }
        isCarrying = !isCarrying;
    }
    public float GetMoveSpeed() { return moveSpeed; }
    public bool GetIsCarrying() { return isCarrying; }
    public bool GetIsRepairing() { return isRepairing; }
}
