using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeShootPlayerBehavior : ShootPlayerBehavior
{

    [Range(0, 180f)]
    public float offsetAngle = 20f;

    protected override void OnFireBullet(Vector3 target)
    {
        base.OnFireBullet(target);

        EnemyBulletBehavior bullet1 = GetBullet(target);
        bullet1.transform.Rotate(offsetAngle * Vector3.forward);
        bullet1.Fire();

        EnemyBulletBehavior bullet2 = GetBullet(target);
        bullet2.transform.Rotate(-offsetAngle * Vector3.forward);
        bullet2.Fire();
    }

}
