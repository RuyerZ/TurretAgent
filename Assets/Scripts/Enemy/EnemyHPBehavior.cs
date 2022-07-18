using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBehavior : MonoBehaviour
{
    public int mMaxHP = 5;
    private int mCurrentHP = 0;
    private EnemyManager mEnemyManager = null;
    // Start is called before the first frame update
    void Start()
    {
        mEnemyManager = GameManager.sTheGlobalBehavior.mEnemyManager;
        mCurrentHP = mMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit(int damage)
    {
        mCurrentHP -= damage;
        if (mCurrentHP <= 0)
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        mEnemyManager.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
