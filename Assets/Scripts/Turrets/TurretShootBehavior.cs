using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootBehavior : TurretAttackBase
{
    public FriendBulletBehavior _BulletPre;
    public Transform _Gun;
    public Transform _Muzzle;
    public Transform _RadiusItem;

    private Transform _Target;
    private void Awake()
    { 
        _AttackIntervalReset = _AttackInterval;
    }

    private void Update()
    {
        TargetUpdate();
        GunRotationUpdate();
        AttackUpdate();
        RadiusUpdate();
    }

    private void TargetUpdate()
    {
        if (_Target == null)
        {
            GameObject t = GameManager.sTheGlobalBehavior.mEnemyManager.GetClosestEnemy(_Gun.position);
            if (t != null)
            {
                _Target = t.transform;
            }
            //_Enemy = _Target.GetComponentInParent<EnemyBehavior>();
        }
        if (_Target != null)
        {
            if (Vector3.Distance(_Target.position, _Gun.position) >= _AttackRadius) //+ _Enemy.GetRadius())
            {
                _Target = null;
            }
        }
    }

    private void GunRotationUpdate()
    {
        if (_Target == null)
        {
            return;
        }

        Vector2 targetDirection = _Target.position - _Gun.position;
        float targetangle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        _Gun.rotation = Quaternion.Euler(0, 0, targetangle);
    }


    private void AttackUpdate()
    {
        if (_AttackIntervalReset > 0)
        {
            _AttackIntervalReset -= Time.deltaTime;
        }
        else
        {
            if (_Target != null)
            {
                shootAudio.Play();
                _AttackIntervalReset = _AttackInterval;

                GetBullet().Fire();
                OnFireBullet();
            }
        }
    }

    //  �����ӵ�
    protected virtual void OnFireBullet() { }

    //  ��ȡ�ӵ�
    protected FriendBulletBehavior GetBullet()
    {
        FriendBulletBehavior bullet = Instantiate(_BulletPre, _Muzzle.position, _Muzzle.rotation);
        bullet.dmg = _AttackDamage;
        return bullet;
    }

    private void RadiusUpdate()
    {
        if (_RadiusItem.localScale != _AttackRadius * Vector3.one)
        {
            _RadiusItem.localScale = _AttackRadius * Vector3.one;
        }
    }

}
