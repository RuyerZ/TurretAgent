using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehavior : FriendBulletBehavior
{
    public float explosionRadius = 3f;

    public override void onHit(GameObject o)
    {
        if (o.GetComponent<EnemyHPBehavior>() == null) return;

        //explosion
        Vector2 center = transform.position;

        List<GameObject> enemies = GameManager.sTheGlobalBehavior.mEnemyManager.GetEnemiesInRadius(center, explosionRadius);
        
        foreach (GameObject enemy in enemies)
        {
            if (GameObject.ReferenceEquals(o, enemy)) {
                continue;
            }
            EnemyHPBehavior enemyBehavior = enemy.GetComponent<EnemyHPBehavior>();
            if ( enemyBehavior != null) {
                enemyBehavior.DamageEnemy(dmg);
            }
        }
        Destroy(gameObject);
    }
}
