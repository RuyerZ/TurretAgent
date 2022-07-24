using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBehavior : MonoBehaviour {
    public float maxHP = 5.0f;
    public float Gold = 1f;
    public bool DefeatToWin = false;
    private float currentHP;
    void Start() {
        currentHP = maxHP;
        Debug.Assert(gameObject.GetComponent<BoxCollider2D>() != null);
        if (GetComponent<PathBehavior>() == null) {
            GameManager.sTheGlobalBehavior.mEnemyManager.AddEnemy(gameObject);
        }
    }
    void CollisionCheck(GameObject o) {
        // Update HP Bar
        FriendBulletBehavior f = o.GetComponent<FriendBulletBehavior>();
        //  子弹
        if (f != null)
        {
            currentHP -= f.getDmg(gameObject);
            if (currentHP <= 0)
                f.onKill(gameObject);
            f.onHit(gameObject);

            UpdateHP();
        }
    }

    //  检测塔的攻击
    private void CheckTurretHit(Collider2D other)
    {
        //  AOE塔
        if (other.CompareTag("RadiusTurret"))
        {
           var turret =  other.GetComponentInParent<RadiusTurretBehavior>();
            if (turret != null)
            {
                currentHP -= turret.AttackDamage;
                UpdateHP();
            }
        }
    }

    //  更新生命
    private void UpdateHP()
    {
        HPBar hp = GetComponentInChildren<HPBar>();
        if (hp != null) { hp.Set(currentHP / maxHP); }
        if (currentHP <= 0)
        {
            GameManager.sTheGlobalBehavior.mEnemyManager.RemoveEnemy(gameObject);
            GameManager.sTheGlobalBehavior.AddGold(Gold);
            if (DefeatToWin)
            {
                GameManager.sTheGlobalBehavior.GameWin();
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        CollisionCheck(other.gameObject);
        CheckTurretHit(other);
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
            GameManager.sTheGlobalBehavior.AddGold(Gold);
            if (DefeatToWin) {
                GameManager.sTheGlobalBehavior.GameWin();
            }
            Destroy(gameObject);
        }
    }
    public string GetHPString() {
        return (currentHP.ToString() + " / " + maxHP.ToString());
    }
}