using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPBehavior : MonoBehaviour {
    //public float maxHP = 10.0f;
    //private float currentHP;
    void Start() {
        //currentHP = maxHP;
        //Debug.Assert(gameObject.GetComponent<BoxCollider2D>() != null);
    }
    void CollisionCheck(GameObject o) {
        if (o.GetComponent<EnemyBulletBehavior>() != null) {
            EnemyBulletBehavior f = o.GetComponent<EnemyBulletBehavior>();
            GameManager.sTheGlobalBehavior.ReduceBaseHP(f.getDmg(gameObject));
            f.onHit(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        CollisionCheck(other.gameObject);
    }
    void OnCollisionEnter2D(Collision2D other) {
        CollisionCheck(other.gameObject);
    }
}