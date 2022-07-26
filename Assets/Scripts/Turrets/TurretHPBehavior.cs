using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHPBehavior : MonoBehaviour
{
    public float maxHP = 10f;
    private float currentHP;
    void Start()
    {
        currentHP = maxHP;
        GameManager.sTheGlobalBehavior.mFriendManager.AddTurret(gameObject);
    }
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            gameObject.GetComponent<TurretShootBehavior>().enabled = false;
            //GameManager.sTheGlobalBehavior.mFriendManager.RemoveTurret(gameObject);
        }

        HPBar hp = GetComponentInChildren<HPBar>();
        if (hp != null)
            hp.Set(currentHP / maxHP);
    }
    public void Repair(float repair)
    {
        currentHP += repair;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        if (currentHP > 0)
        {
            gameObject.GetComponent<TurretAttackBase>().enabled = true;
            //GameManager.sTheGlobalBehavior.mFriendManager.AddTurret(gameObject);
        }
        HPBar hp = GetComponentInChildren<HPBar>();
        if (hp != null)
            hp.Set(currentHP / maxHP);
    }
    public float GetCurrentHP()
    {
        return currentHP;
    }
    void CollisionCheck(GameObject other)
    {
        if (other.GetComponent<EnemyBulletBehavior>() != null)
        {
            EnemyBulletBehavior t = other.GetComponent<EnemyBulletBehavior>();
            TakeDamage(t.getDmg(gameObject));
            t.onHit(gameObject);
            //HPBar hp = GetComponentInChildren<HPBar>();
            //if (hp != null)
            //    hp.Set(currentHP / maxHP);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        CollisionCheck(other.gameObject);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        CollisionCheck(other.gameObject);
    }
}