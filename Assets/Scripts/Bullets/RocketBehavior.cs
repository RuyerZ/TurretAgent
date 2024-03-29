using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    public float explosionRadius = 3f;
    public float dmg = 5f;
    public float fireforce = 20f;
    //using position when shot + distance travelled to trigger explosion, more robust
    private Vector2 shootPosition;
    private float explodeDistance;
    public Animator Anim;
    public Collider2D Coll;
    public AudioSource explodeAudio;

    public Rigidbody2D rigid;

    void Start()
    {
        Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();
        explodeAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (shootPosition != null)
        {
            Vector2 pos = transform.position;

            if (Vector2.Distance(pos, shootPosition) > explodeDistance)
            {
                onHit();
            }
        }
    }

    public void Fire()
    {
        shootPosition = transform.position;
        Vector2 mousePoistion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v = (mousePoistion - shootPosition);
        Vector2 dir = v.normalized;
        explodeDistance = v.magnitude;


        GetComponentInChildren<Rigidbody2D>().AddForce(dir * fireforce, ForceMode2D.Impulse);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<EnemyHPBehavior>() != null)
        {
            onHit();
        }
    }

    public void onHit()
    {
        if (!Coll.enabled)
        {
            return;
        }
        rigid.simulated = false;

        //explosion
        Vector2 center = transform.position;

        List<GameObject> enemies = GameManager.sTheGlobalBehavior.mEnemyManager.GetEnemiesInRadius(center, explosionRadius);

        foreach (GameObject enemy in enemies)
        {
            EnemyHPBehavior enemyBehavior = enemy.GetComponent<EnemyHPBehavior>();
            if (enemyBehavior != null)
            {
                enemyBehavior.DamageEnemy(dmg);
            }
        }

        Coll.enabled = false;
        //gameObject.SetActive(false);
        Anim.SetTrigger("explode");
        explodeAudio.Play();
    }

    void destroyRocket()
    {
        Destroy(gameObject);
    }

}
