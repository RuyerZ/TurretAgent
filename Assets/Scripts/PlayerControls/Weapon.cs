using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    private WeaponType weaponType;
    public Sprite pistolSprite;
    public Sprite rifleSprite;

    void Awake()
    {
        SetWeapon("Pistol");
    }

    void Update()
    {
        //sprite manipulation for visuals
        float angle = transform.parent.rotation.eulerAngles.z;
        Vector3 scale = transform.localScale;
        Vector3 pos = transform.position;
        //flip sprite
        if (0f <= angle && angle <= 180f && transform.localScale.x > 0f) {
             scale.x *= -1;
        }
        if (180f < angle && angle <= 360f && transform.localScale.x < 0f) {
             scale.x *= -1;
        }
        //move layer
        if (0f <= angle && angle < 90f || 270f < angle && angle <= 360f) {
            pos.z = 0.001f;
        } else {
            pos.z = -0.001f;
        }
        transform.localScale = scale;
        transform.position = pos;
    }

    public void Fire()
    {
        weaponType.Fire(firePoint);
    }

    public void SetWeapon(string t)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        switch (t)
        {
            case "Pistol":
                weaponType = new Pistol();
                spriteRenderer.sprite = pistolSprite;
                break;
            case "Rifle":
                weaponType = new Rifle();
                spriteRenderer.sprite = rifleSprite;
                break;
            default:
                Debug.Log("Something went Wrong at SetWeapon");
                break;
        }
    }
    
    //store all the weapon types
    public abstract class WeaponType
    {
        public string type;
        protected string bulletType = "Bullet";
        protected float fireForce = 20f;
        protected float cooldownDuration = 1.0f;
        protected float timeStamp;
        
        public void Fire(Transform fp) {
            if (Time.time < timeStamp) return;

            GameObject bullet = Instantiate(Resources.Load("Prefabs/Bullets/" + bulletType) as GameObject, 
                fp.position, fp.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(fp.up * fireForce, ForceMode2D.Impulse);
            Debug.Log(cooldownDuration);
            Debug.Log(type);

            timeStamp = Time.time + cooldownDuration;
        }
    }
    public class Pistol : WeaponType
    {
        public Pistol() {
            type = "Pistol";
            bulletType = "Bullet";
            fireForce = 20f;
            cooldownDuration = 1.0f;
        }
    }
    public class Rifle : WeaponType
    {
        public Rifle() {
            type = "Rifle";
            bulletType = "Bullet";
            fireForce = 25f;
            cooldownDuration = 0.2f;
        }
    }

}
