using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEnemyBehavior : MonoBehaviour
{
    public float damageValueToPlayer = 1f;
    public float damageValueToTower = 15f;

    public float explosionRadius = 3f;
    public AudioSource explodeSound;
    public Transform explodeGo;
    private Animator Anim;
    private Collider2D Coll;

    private float k1 = 0.2f;
    private float k2 = 0.7f;
    private float k3 = 0.2f;

    private float towerDistance;
    private float pathDistance;

    private PathBehavior pathBehavior;

    protected virtual void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        Coll = GetComponent<Collider2D>();
        pathBehavior = GetComponentInChildren<PathBehavior>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Explode();
        //}
        if (IsValid())
        {
            Explode();
        }
    }


    private bool IsValid()
    {
        towerDistance = GetTowerDistance();
        pathDistance = pathBehavior != null ? pathBehavior.PathDistance : 0;//防止被除数等于0；

        float ratio = k1 * Time.smoothDeltaTime * (float)System.Math.Tanh(k2 * towerDistance + k3 * pathDistance);
        //Debug.Log("概率：" + ratio);

        return Random.Range(0, 1f) < ratio;
    }

    private float GetTowerDistance()
    {
        float dist = float.MaxValue;
        TurretHPBehavior[] turrets = FindObjectsOfType<TurretHPBehavior>();
        float temp;
        for (int i = 0; i < turrets.Length; i++)
        {
            temp = Vector3.Distance(transform.position, turrets[i].transform.position);
            if (dist > temp)
            {
                dist = temp;
            }
        }
        return dist == 0 ? 1 : dist;
    }

    private void Explode()
    {
        if (Coll.enabled == false)
            return;

        if (GetComponentInChildren<EnemyHPBehavior>() == null)
            return;

        if (explodeSound != null)
            explodeSound.Play();

        Coll.enabled = false;
        Anim.SetBool("explode", true);
        explodeGo.localScale = explosionRadius * Vector3.one;

        HPBar hp = GetComponentInChildren<HPBar>();
        hp.gameObject.SetActive(false);
        PathBehavior enemy_move = GetComponent<PathBehavior>();
        if (enemy_move != null)
        {
            enemy_move.enabled = false;
        }

        //  伤害
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector3.zero);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.CompareTag("Turret"))
            {
                TurretHPBehavior turret = hits[i].collider.GetComponentInParent<TurretHPBehavior>();
                if (turret != null)
                {
                    float dist = Vector3.Distance(transform.position, turret.transform.position);
                    turret.TakeDamage((float)damageValueToTower/dist);
                }
            }
            else if (hits[i].collider.CompareTag("Player"))
            {
                PlayerHPBehavior player = hits[i].collider.GetComponentInParent<PlayerHPBehavior>();
                if (player != null)
                {
                    player.TakeDamage(damageValueToPlayer);
                }
            }
        }
    }
    public void Destroy() {
        GameManager.sTheGlobalBehavior.mEnemyManager.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }
}
