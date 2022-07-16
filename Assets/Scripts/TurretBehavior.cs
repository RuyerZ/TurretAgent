using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Weapon weapon;

    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        FixedUpdate();
    }

    private bool FixedUpdate()
    {
        if (target != null) {
            Vector2 targetDirection = target.transform.position - transform.position;
            float targetangle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, targetangle);
        }
        return false;
    }
}
