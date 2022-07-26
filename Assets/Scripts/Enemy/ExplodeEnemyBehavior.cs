using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEnemyBehavior : MonoBehaviour
{
    public float damageValue = 1f;

    public float explosionRadius = 3f;
    public AudioSource explodeSound;
    public Transform explodeGo;
    private Animator Anim;
    private Collider2D Coll;

    [Range(0, 10f)]
    public float k1 = 2f;
    public float k2 = 1f;
    public float k3 = 1f;

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

        float ratio = k1 * Time.smoothDeltaTime * (float)System.Math.Tanh(k2 / k3 * towerDistance + pathDistance);
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


        //  伤害
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector3.zero);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.CompareTag("Turret"))
            {
                TurretHPBehavior turret = hits[i].collider.GetComponentInParent<TurretHPBehavior>();
                if (turret != null)
                {
                    turret.TakeDamage(damageValue);
                }
            }
            else if (hits[i].collider.CompareTag("Player"))
            {
                PlayerHPBehavior player = hits[i].collider.GetComponentInParent<PlayerHPBehavior>();
                if (player != null)
                {
                    player.TakeDamage(damageValue);
                }
            }
        }

    }


    public void Destroy()
    {
        Destroy(gameObject);
    }
}
