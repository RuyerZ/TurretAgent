using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    Vector2 mousePoistion;
    public Weapon weapon;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        mousePoistion = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 pos = transform.position;
        Vector2 aimDirection = mousePoistion - pos;
        
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        //transform.Rotate(transform.parent.position, 0, aimAngle);
        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }
}
