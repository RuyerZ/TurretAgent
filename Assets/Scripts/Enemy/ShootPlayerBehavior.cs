using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayerBehavior : MonoBehaviour
{
    public EnemyBulletBehavior bulletPrefab;
    public AudioSource shootAudio;
    public float mShootCooldown = 1.0f;
    private float mLastShotTime = 0f;
    public float mShootFromRadius = 0.7f;

    void Start()
    {
        shootAudio = GetComponent<AudioSource>();
    }
    void Fire(Vector3 target)
    {
        shootAudio.Play();
        //EnemyBulletBehavior b = Instantiate(bulletPrefab, transform.position, transform.rotation);
        //Vector2 direction = target - transform.position;
        //b.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
        //b.transform.position = b.transform.position + b.transform.up * mShootFromRadius;
        GetBullet(target).Fire();
        OnFireBullet(target);
    }

    protected virtual void OnFireBullet(Vector3 target) { }

    //  »ñÈ¡×Óµ¯
    protected EnemyBulletBehavior GetBullet(Vector3 target)
    {
        EnemyBulletBehavior b = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Vector2 direction = target - transform.position;
        b.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f);
        b.transform.position = b.transform.position + b.transform.up * mShootFromRadius;

        return b;
    }

    void Update()
    {
        if (Time.time - mLastShotTime < mShootCooldown)
            return;
        if (GameManager.sTheGlobalBehavior.mHero != null)
        {
            Fire(GameManager.sTheGlobalBehavior.mHero.transform.position);
            mLastShotTime = Time.time;
        }
    }
}
