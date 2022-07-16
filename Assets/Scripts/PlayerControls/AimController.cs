using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public Weapon weapon;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        PlayerController p = gameObject.GetComponentInParent<PlayerController>();

        float aimAngle = p.GetAimAngle();

        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }
}
