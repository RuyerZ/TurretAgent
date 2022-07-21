using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeTurretShootBehavior : TurretShootBehavior
{
    [Range(0, 180f)]
    public float offsetAngle = 20f;

    protected override void OnFireBullet()
    {
        base.OnFireBullet();

        FriendBulletBehavior bullet1 = GetBullet();
        bullet1.transform.Rotate(offsetAngle * Vector3.forward);
        bullet1.Fire();

        FriendBulletBehavior bullet2 = GetBullet();
        bullet2.transform.Rotate(-offsetAngle * Vector3.forward);
        bullet2.Fire();
    }

}
