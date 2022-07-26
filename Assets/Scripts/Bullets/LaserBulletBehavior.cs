using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBulletBehavior : EnemyBulletBehavior
{
    public override void Fire()
    {

    }
    public override void onHit(GameObject o)
    {
        Debug.Log("Laser");
    }
}