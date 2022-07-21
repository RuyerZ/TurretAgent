using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : ItemBase
{
    public FriendBulletBehavior Bullet;
    public AudioSource placeAudio;
    public float cooldownDuration = 0.3f;
    private float timeStamp = 0;

    void Update()
    {
        //FUTURE: ADD VALID CHECK LIKE TURRET

        //stop rendering if no ammo
        SpriteRenderer r = GetComponent<SpriteRenderer>();

        r.enabled = (itemCount != 0);

        transform.rotation = Quaternion.Euler(0f, 0f, -90f);

    }

    public override void Fire() 
    {

        if (Time.time < timeStamp) return;
        if (itemCount == 0) return;

        placeAudio.Play();
        Instantiate(Bullet, firePoint.position, firePoint.rotation).Fire();
        if (itemCount > 0) itemCount--;

        timeStamp = Time.time + cooldownDuration;
    }
}
