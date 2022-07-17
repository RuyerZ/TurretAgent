using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float _Force = 20f;

    private void Awake()
    {
        
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
    }

}
