using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//infinite ammo weapons
public class WeaponBase : ItemBase
{
    public FriendBulletBehavior Bullet;
    public float cooldownDuration = 0.25f;
    float timeStamp = 0;
    
    override public void Fire() 
    {
        if (Time.time < timeStamp) return;

        Instantiate(Bullet, firePoint.position, firePoint.rotation).Fire();

        timeStamp = Time.time + cooldownDuration;
    }
}
