using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFireCreate : MonoBehaviour
{
    public GhostFireBulletBehavior ghostfire;
    public AudioSource shootAudio;
    public float mShootTime = 2.0f;
    private float mShootFromRadius = 0.7f;
    private bool mShoot = false;
    // Start is called before the first frame update
    void Start()
    {
        shootAudio = GetComponent<AudioSource>();
    }

    void Fire(Vector3 target)
    {
        shootAudio.Play();
        GhostFireBulletBehavior b = Instantiate(ghostfire, transform.position, transform.rotation);
        Vector2 direction = target - transform.position;
        b.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
        b.transform.position = b.transform.position + b.transform.up * mShootFromRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mShoot)    
        {
            if (mShootTime >= 0)
                mShootTime -= Time.smoothDeltaTime;
            else
            {
                if (GameManager.sTheGlobalBehavior.mHero != null)
                {
                    Fire(GameManager.sTheGlobalBehavior.mHero.transform.position);
                }

                mShoot = true;
            }
        }
    }
}
