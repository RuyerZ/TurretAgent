using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeShootPlayerBehavior : ShootPlayerBehavior
{

    [Range(0, 180f)]
    public float offsetAngle = 20f;
    public int bulletFactor = 1;

    protected override void OnFireBullet(Vector3 target)
    {
        base.OnFireBullet(target);

        for (int i = 1; i <= bulletFactor; i++) {
            EnemyBulletBehavior bullet1 = GetBullet(target);
            bullet1.transform.Rotate(offsetAngle * i * Vector3.forward);
            bullet1.Fire();
        }
        for (int i = 1; i <= bulletFactor; i++) {
            EnemyBulletBehavior bullet1 = GetBullet(target);
            bullet1.transform.Rotate(-offsetAngle * i * Vector3.forward);
            bullet1.Fire();
        }
    }

}
