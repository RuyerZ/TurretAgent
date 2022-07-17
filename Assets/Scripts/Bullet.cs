using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _Force = 20f;

    private void Awake()
    {
        
    }

    void Update()
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

    public void Fire()
    {
        GetComponentInChildren<Rigidbody2D>().AddForce(transform.up * _Force, ForceMode2D.Impulse);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<EnemyBehavior>())
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "dummy")
        {
            Destroy(gameObject);
        }
    }

}
