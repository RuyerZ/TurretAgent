using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public GameObject target;
    public GameObject mBulletPrefab;
    public float mShootCooldown;
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
        float targetangle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Rotate(targetangle);
        return true;
    }
    private bool TargetUpdate() {
        if (target == null) {
            target = GameManager.sTheGlobalBehavior.mEnemyManager.GetClosestEnemy(transform.position);
            if (target == null) return false;
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
        GameObject bullet = Instantiate(mBulletPrefab, transform.position + transform.up * 0.1f, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * 20f, ForceMode2D.Impulse);
        LastShotTime = Time.time;
    }
}
