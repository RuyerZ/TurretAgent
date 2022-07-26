using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBehavior : MonoBehaviour
{
    public float maxHP = 5.0f;
    public float Gold = 1f;
    public bool DefeatToWin = false;
    public float damageBlock = 0;
    private float currentHP;
    public Animator animator;
    void Start()
    {
        currentHP = maxHP;
        Debug.Assert(gameObject.GetComponent<Collider2D>() != null);
        if (GetComponent<PathBehavior>() == null)
        {
            GameManager.sTheGlobalBehavior.mEnemyManager.AddEnemy(gameObject);
        }
    }


    void CollisionCheck(GameObject o)
    {
        // Update HP Bar
        FriendBulletBehavior f = o.GetComponent<FriendBulletBehavior>();
        //  �ӵ�
        if (f != null)
        {
            float trueDmg = f.getDmg(gameObject) - damageBlock;
            if (trueDmg < 0)
                trueDmg = 0;
            currentHP -= trueDmg;
            if (currentHP <= 0)
                f.onKill(gameObject);
            f.onHit(gameObject);

            UpdateHP();
        }
    }

    //  ������Ĺ���
    private void CheckTurretHit(Collider2D other)
    {
        //  AOE��
        if (other.CompareTag("RadiusTurret"))
        {
            var turret = other.GetComponentInParent<RadiusTurretBehavior>();
            if (turret != null)
            {
                currentHP -= turret.AttackDamage;
                UpdateHP();
            }
        }
    }

    //  ��������
    protected virtual void UpdateHP()
    {
        HPBar hp = GetComponentInChildren<HPBar>();
        if (hp != null)
        {
            hp.Set(currentHP / maxHP);
        }
        if (currentHP <= 0)
        {
            GameManager.sTheGlobalBehavior.mEnemyManager.RemoveEnemy(gameObject);
            GameManager.sTheGlobalBehavior.AddGold(Gold);
            if (DefeatToWin)
            {
                if (animator != null) {
                    transform.localScale = Vector3.one * 5f;
                    hp.gameObject.SetActive(false);
                    animator.SetTrigger("explode");
                    Invoke("DelayInvole", 0.7f);
                } else {
                    GameManager.sTheGlobalBehavior.GameWin();
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }


        }
    }

    private void DelayInvole()
    {
        Destroy(gameObject);
        GameManager.sTheGlobalBehavior.GameWin();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        CollisionCheck(other.gameObject);
        CheckTurretHit(other);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        CollisionCheck(other.gameObject);
    }
    public void DamageEnemy(float dmg)
    {
        float trueDmg = dmg - damageBlock;
        if (trueDmg < 0)
            trueDmg = 0;
        Debug.Log(trueDmg);
        currentHP -= trueDmg;
        // Update HP Bar
        HPBar hp = GetComponentInChildren<HPBar>();
        if (hp != null)
            hp.Set(currentHP / maxHP);

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
    public string GetHPString()
    {
        return (currentHP.ToString("N1") + " / " + maxHP.ToString());
    }
}