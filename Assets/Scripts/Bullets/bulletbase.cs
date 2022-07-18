using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface BulletInterface
{
    float getDmg(GameObject o);
    void onHit(GameObject o);
    void onKill(GameObject o);
}

public class BaseBullet : MonoBehaviour, BulletInterface
{
    public float dmg = 1f;
    public float fireforce = 20f;
    public void Fire()
    {
        GetComponentInChildren<Rigidbody2D>().AddForce(transform.up * fireforce, ForceMode2D.Impulse);
    }
    public float getDmg(GameObject o)
    {
        return dmg;
    }
    public void onHit(GameObject o)
    {
        Destroy(gameObject);
    }
    public void onKill(GameObject o) {} // Preserve for increase EXP
}