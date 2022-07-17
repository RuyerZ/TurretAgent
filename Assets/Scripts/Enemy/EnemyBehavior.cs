using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Animator _Anim;
    private enum AnimState { Idle, Down, Up, Left, Right }

    public GameObject bulletPrefab;
    public int maxHP = 5;
    public float pathSpeed = 1.0f;

    private PathSystem pathSystem = null;
    public string pathName;
    public void SetPath(string n) {pathName = n;}
    private int currentHP;
    private float pathDistance;
    private float fireForce = 20.0f;
    // Start is called before the first frame update

    private void Awake()
    {
        _Anim = GetComponentInChildren<Animator>();
        _LastPoint = transform.position;
        _RadiusC.enabled = false;
    }

    void Start()
    {
        pathSystem = GameManager.sTheGlobalBehavior.mPathSystem;
        GameManager.sTheGlobalBehavior.mEnemyManager.AddEnemy(gameObject);
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
        UpdateAnim();
    }

    private void Fire()
    {
        GameObject b = Instantiate(bulletPrefab, transform.position, transform.rotation);
        b.GetComponent<Rigidbody2D>().AddForce(transform.up, ForceMode2D.Impulse);
    }

    private void UpdatePosition()
    {
        //Debug.Assert(pathSystem != null);
        //Debug.Assert(pathName != null && pathSystem.PathExists(pathName));
        Debug.Log(pathSystem == null);
        transform.position = pathSystem.GetPositionFromPath(pathName, pathDistance);
        pathDistance += pathSpeed * Time.smoothDeltaTime;
    }

    private Vector3 _LastPoint;
    //  ���¶���
    private void UpdateAnim()
    {
        if (_Anim == null) { return; }
        AnimState state = AnimState.Idle;

        if (transform.position == _LastPoint) { state = AnimState.Idle; }
        else
        {
            if (transform.position.x < _LastPoint.x) { state = AnimState.Left; }
            else if (transform.position.x > _LastPoint.x) { state = AnimState.Right; }
            else if (transform.position.x == _LastPoint.x)
            {
                if (transform.position.y < _LastPoint.y) { state = AnimState.Down; }
                else { state = AnimState.Up; }
            }
        }

        _Anim.SetInteger("State", (int)state);
        _LastPoint = transform.position;
    }

    //  ��ȡ��Χ�뾶
    [SerializeField]
    private CircleCollider2D _RadiusC = null;
    public float GetRadius()
    {
        return _RadiusC.radius * _RadiusC.transform.lossyScale.x;
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
        GameManager.sTheGlobalBehavior.mEnemyManager.RemoveEnemy(gameObject);
        Destroy(gameObject);
    }
}