using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKiller : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float mShootCooldown = 1.0f;
    private float mLastShotTime = 0f;
    public float radius = 0f;
    void Start() {
        if (radius == 0) radius = gameObject.GetComponent<EnemyBehavior>().GetRadius();
    }
    void Fire(Vector3 target) {
        Bullet b = Instantiate(bulletPrefab, transform.position,transform.rotation);
        b._Force = 5f;
        Vector2 direction = target - transform.position;
        b.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
        b.transform.position = b.transform.position + b.transform.up * radius;
        b.Fire();
    }
    void Update() {
        if (Time.time - mLastShotTime < mShootCooldown)
            return;
        if (GameManager.sTheGlobalBehavior.mHero != null) {
            Fire(GameManager.sTheGlobalBehavior.mHero.transform.position);
            mLastShotTime = Time.time;
        }
    }
}
