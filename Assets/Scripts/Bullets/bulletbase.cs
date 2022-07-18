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
    public void Update()
    {
        // 1. Find the main camera and get the CameraSupport component
        CameraSupport s = Camera.main.GetComponent<CameraSupport>();  // Try to access the CameraSupport component on the MainCamera
        if (s != null)   // if main camera does not have the script, this will be null
        {
            // intersect my bond with the bounds of the world
            Bounds myBound = GetComponent<Renderer>().bounds;  // this is the bound on the SpriteRenderer
            CameraSupport.WorldBoundStatus status = s.CollideWorldBound(myBound);
            
            // If result is not "inside", then, destroy
            if (status != CameraSupport.WorldBoundStatus.Inside)
            {
                Destroy(gameObject);
            }
        }
    }
}