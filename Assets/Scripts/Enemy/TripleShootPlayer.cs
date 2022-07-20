using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShootPlayer : MonoBehaviour
{
    public EnemyBulletBehavior bulletPrefab;
    public AudioSource shootAudio;
    public float mShootCooldown = 1.0f;
    private float mLastShotTime = 0f;
    public float mShootFromRadius = 0.7f;
    public float mShootAngle = 30.0f;

    void Start()
    {
        shootAudio = GetComponent<AudioSource>();
    }
    void Fire(float angle)
    {
        shootAudio.Play();
        EnemyBulletBehavior b = Instantiate(bulletPrefab, transform.position, transform.rotation);
        b.transform.rotation = Quaternion.Euler(0, 0, angle);
        b.transform.position = b.transform.position + b.transform.up * mShootFromRadius;
        b.Fire();
    }
    void Update()
    {
        if (Time.time - mLastShotTime < mShootCooldown)
            return;
        if (GameManager.sTheGlobalBehavior.mHero != null)
        {
            Vector3 target = GameManager.sTheGlobalBehavior.mHero.transform.position;
            Vector2 direction = target - transform.position;
            float centerAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Fire(centerAngle - mShootAngle);
            Fire(centerAngle);
            Fire(centerAngle + mShootAngle);
            mLastShotTime = Time.time;
        }
    }
}
