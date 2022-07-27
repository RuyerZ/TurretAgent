using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBulletBehavior : EnemyBulletBehavior
{
    public float mStartTime = 4.0f;
    public float mDestroyTime = 16.0f;
    public float animTime = 0.2f;
    public AudioSource laserAudio;
    public Animator animator;
    private SpriteRenderer mSprite;
    private SpriteRenderer warn;
    private BoxCollider2D mBoxCollider;
    private float mDamage = 0.0f;
    private float mTimer = 0.0f;
    private bool mStart = false;

    public void Start()
    {
        mSprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        warn = transform.Find("warn").GetComponent<SpriteRenderer>();
        mBoxCollider = GetComponent<BoxCollider2D>();

        mSprite.enabled = false;
        warn.enabled = true;

        // if (mSprite != null)
        // {
        //     Color color = mSprite.color;
        //     color.a = 0.5f;
        //     mSprite.color = color;
        // }

        if (mBoxCollider != null)
            mBoxCollider.enabled = false;

        mDamage = dmg;
        dmg = 0f;
    }

    public new void Update()
    {
        if (mTimer > mDestroyTime)
            Destroy(gameObject);

        if (!mStart && mTimer < mStartTime) {
            Color color = warn.color;
            color.a = (mTimer / mStartTime);
            warn.color = color;
        }
        

        if (!mStart && mTimer > mStartTime)
        {
            if (mSprite != null)
            {
                mSprite.enabled = true;
            }
        }
        if (!mStart && mTimer > mStartTime + animTime) {
            
            if (mBoxCollider != null)
                mBoxCollider.enabled = true;

            mStart = true;
            dmg = mDamage;

            animator.SetBool("next",true);
            if (laserAudio) laserAudio.Play();

            warn.enabled = false;
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