using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int maxHP = 5;
    public float pathSpeed = 1.0f;

    public PathSystem pathSystem;
    public string pathName;
    public void SetPath(PathSystem p, string n) { pathSystem = p; pathName = n; }

    private int currentHP;
    private float pathDistance;
    private float fireForce = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(pathSystem != null);
        Debug.Assert(bulletPrefab != null);
        Debug.Assert(maxHP > 0);

        currentHP = maxHP;
        pathDistance = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    private void Fire()
    {
        GameObject b = Instantiate(bulletPrefab, transform.position, transform.rotation);
        b.GetComponent<Rigidbody2D>().AddForce(transform.up, ForceMode2D.Impulse);
    }

    private void UpdatePosition()
    {
        Debug.Assert(pathSystem != null);
        Debug.Assert(pathName != null && pathSystem.PathExists(pathName));

        transform.position = pathSystem.GetPositionFromPath(pathName, pathDistance);
        pathDistance += pathSpeed * Time.smoothDeltaTime;
    }

    public void Hit()
    {
        currentHP--;
        if (currentHP <= 0)
        {
            DestroySelf();
        }
    }

    public void DestroySelf()
    {
        Destroy(transform.gameObject);
    }
}
