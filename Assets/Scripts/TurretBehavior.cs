using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public GameObject target;
    public GameObject mBulletPrefab;
    public float mShootCooldown;
    public float mShootRange;
    public float mShootForce;
    private float LastShotTime;
    // Update is called once per frame
    void Update()
    {
        FixedUpdate();
        TargetUpdate();
        FireUpdate();
    }

    private bool FixedUpdate()
    {
        if (target == null) return false;
        if (!GameManager.sTheGlobalBehavior.mEnemyManager.mEnemies.Contains(target)) {
            target = null;
            return false;
        }
        Vector2 targetDirection = target.transform.position - transform.position;
        if (targetDirection.magnitude > mShootRange) {
            target = null;
            return false;
        }
        float targetangle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Rotate(targetangle);
        return true;
    }
    private bool TargetUpdate() {
        if (target == null) {
            target = GameManager.sTheGlobalBehavior.mEnemyManager.GetClosestEnemy(transform.position);
            if (target == null) return false;
            if ((target.transform.position-transform.position).magnitude > mShootRange) {
                target = null;
                return false;
            }
            return true;
        }
        return false;
    }
    private void Rotate(float angle) {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void FireUpdate() {
        if (target == null) return;
        if (Time.time - LastShotTime < mShootCooldown) return;
        GameObject bullet = Instantiate(mBulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * mShootForce, ForceMode2D.Impulse);
        LastShotTime = Time.time;
    }
}
