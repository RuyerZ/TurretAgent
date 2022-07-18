using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHPBehavior : MonoBehaviour {
    public float maxHP = 10f;
    private float currentHP;
    void Start() {
        currentHP = maxHP;
    }
    public void TakeDamage(float damage) {
        currentHP -= damage;
        if (currentHP <= 0) {
            gameObject.GetComponent<TurretShootBehavior>().enabled = false;
        }
    }
    public void Repair(float repair) {
        currentHP += repair;
        if (currentHP > maxHP) {
            currentHP = maxHP;
        }
        if (currentHP > 0) {
            gameObject.GetComponent<TurretShootBehavior>().enabled = true;
        }
    }
    void CollisionCheck(GameObject other) {
        if (other.GetComponent<EnemyBulletBehavior>() != null) {
            EnemyBulletBehavior t = other.GetComponent<EnemyBulletBehavior>();
            TakeDamage(t.getDmg(gameObject));
            t.onHit(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        CollisionCheck(other.gameObject);
    }
    void OnCollisionEnter2D(Collision2D other) {
        CollisionCheck(other.gameObject);
    }
}