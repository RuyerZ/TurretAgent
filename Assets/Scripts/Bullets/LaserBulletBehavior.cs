using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBulletBehavior : EnemyBulletBehavior
{
    public float mStartTime = 4.0f;
    public float mDestroyTime = 16.0f;
    private SpriteRenderer mSprite;
    private float mDamage = 0.0f;
    private float mTimer = 0.0f;
    private bool mStart = false;

    public void Start()
    {
        mSprite = GetComponentInChildren<SpriteRenderer>();
        if (mSprite != null)
        {
            Color color = mSprite.color;
            color.a = 0.5f;
            mSprite.color = color;
        }
        mDamage = dmg;
        dmg = 0f;
    }

    public new void Update()
    {
        if (mTimer > mDestroyTime)
            Destroy(gameObject);

        if (!mStart && mTimer > mStartTime)
        {
            if (mSprite != null)
            {
                Color color = mSprite.color;
                color.a = 1f;
                mSprite.color = color;
            }
            mStart = true;
            dmg = mDamage;
        }

        mTimer += Time.smoothDeltaTime;
    }
    public override void Fire()
    {
        if (GameManager.sTheGlobalBehavior.mHero != null)
        {
            Vector3 target = GameManager.sTheGlobalBehavior.mHero.transform.position;
            Vector2 direction = target - transform.position;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f + Random.Range(-30f, 30f));
        }
        else transform.localRotation = Quaternion.Euler(0, 0, 90);
    }
    public override void onHit(GameObject o)
    {
        
    }
}