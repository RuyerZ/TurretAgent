using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBehavior : MonoBehaviour {
    public float maxHP = 5.0f;
    private float currentHP;
    void Start() {
        currentHP = maxHP;
        Debug.Assert(gameObject.GetComponent<BoxCollider2D>() != null);
    }
    void CollisionCheck(GameObject o) {
        if (o.GetComponent<FriendBulletBehavior>() != null) {
            FriendBulletBehavior f = o.GetComponent<FriendBulletBehavior>();
            currentHP -= f.getDmg(gameObject);

            // Update HP Bar
            HPBar hp = GetComponentInChildren<HPBar>();
            if (hp != null)
                hp.Set(currentHP / maxHP);

            if (currentHP <= 0) 
                f.onKill(gameObject);
            f.onHit(gameObject);
            if (currentHP <= 0) {
                GameManager.sTheGlobalBehavior.mEnemyManager.RemoveEnemy(gameObject);
                GameManager.sTheGlobalBehavior.AddGold(1);
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        CollisionCheck(other.gameObject);
    }
    void OnCollisionEnter2D(Collision2D other) {
        CollisionCheck(other.gameObject);
    }
    public void DamageEnemy(float dmg)
    {
        currentHP -= dmg;
        // Update HP Bar
        HPBar hp = GetComponentInChildren<HPBar>();
        if (hp != null)
            hp.Set(currentHP / maxHP);

        if (currentHP <= 0) {
            GameManager.sTheGlobalBehavior.mEnemyManager.RemoveEnemy(gameObject);
            GameManager.sTheGlobalBehavior.AddGold(1);
            Destroy(gameObject);
        }
    }
}