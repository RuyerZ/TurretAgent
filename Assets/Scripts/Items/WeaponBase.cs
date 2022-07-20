using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//infinite ammo weapons
public class WeaponBase : ItemBase
{
    public FriendBulletBehavior Bullet;
    public AudioSource shootAudio;
    public float cooldownDuration = 0.25f;
    private float timeStamp = 0;

    public override void Fire() 
    {

        if (Time.time < timeStamp) return;
        if (itemCount == 0) return;

        shootAudio.Play();
        Instantiate(Bullet, firePoint.position, firePoint.rotation).Fire();
        if (itemCount > 0) itemCount--;

        timeStamp = Time.time + cooldownDuration;
    }
}
