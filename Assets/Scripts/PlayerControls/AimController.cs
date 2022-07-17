using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public Weapon weapon;
    //to be change to a proper UI class in future
    public List<string> itemBar = new List<string>(){"Pistol","Rifle"};

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            weapon.Fire();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon.SetWeapon(itemBar[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon.SetWeapon(itemBar[1]);
        }

        PlayerController p = gameObject.GetComponentInParent<PlayerController>();

        float aimAngle = p.GetAimAngle();

        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }
}
