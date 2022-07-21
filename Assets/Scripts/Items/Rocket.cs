using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//infinite ammo weapons
public class Rocket : ItemBase
{
    public RocketBehavior Bullet;
    public AudioSource shootAudio;
    public float cooldownDuration = 0.7f;
    private float timeStamp = 0;

    public override void Fire() 
    {

        if (Time.time < timeStamp) return;
        if (itemCount == 0) return;

        if (shootAudio != null) shootAudio.Play();
        Instantiate(Bullet, firePoint.position, firePoint.rotation).Fire();
        if (itemCount > 0) itemCount--;

        timeStamp = Time.time + cooldownDuration;
    }
}
