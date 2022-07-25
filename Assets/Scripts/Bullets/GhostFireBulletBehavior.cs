using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFireBulletBehavior : MonoBehaviour, BulletInterface
{
    public float dmg = 1f;
    public float fireforce = 1f;
    public float lifetime = 5f;
    public void Fire()
    {
        
    }
    public float getDmg(GameObject o)
    {
        return dmg;
    }
    public void onHit(GameObject o)
    {
        Destroy(gameObject);
    }
    public void onKill(GameObject o)
    {

    }

    public void Update()
    {
        if (GameManager.sTheGlobalBehavior.isPaused) return;
        // Check lifetime
        lifetime -= Time.smoothDeltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
        // Follow the player
        PlayerMoveBehavior player = GameManager.sTheGlobalBehavior.mHero;
        if (player != null)
        {
            Vector3 target = player.transform.position;
            Vector2 direction = target - transform.position;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
            transform.position = transform.position + transform.up * fireforce;
        }
    }
}
