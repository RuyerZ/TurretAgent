using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Animator _Anim;
    private enum AnimState { Idle, Down, Up, Left, Right }

    public int maxHP = 5;
    public float pathSpeed = 1.0f;

    private PathSystem pathSystem = null;
    public string pathName;
    public void SetPath(string n) { pathName = n; }
    private int currentHP;
    private float pathDistance;
    // Start is called before the first frame update

    public void Awake()
    {
        _Anim = GetComponentInChildren<Animator>();
        _LastPoint = transform.position;
        _RadiusC.enabled = false;
    }

    public void Start()
    {
        pathSystem = GameManager.sTheGlobalBehavior.mPathSystem;
        GameManager.sTheGlobalBehavior.mEnemyManager.AddEnemy(gameObject);
        Debug.Assert(pathSystem != null);
        Debug.Assert(maxHP > 0);

        currentHP = maxHP;
        pathDistance = 0.0f;
    }

    // Update is called once per frame
    public void Update()
    {
        UpdatePosition();
        UpdateAnim();
    }

    private void UpdatePosition()
    {
        //Debug.Assert(pathSystem != null);
        //Debug.Assert(pathName != null && pathSystem.PathExists(pathName));
        transform.position = pathSystem.GetPositionFromPath(pathName, pathDistance);
        pathDistance += pathSpeed * Time.smoothDeltaTime;
    }

    private Vector3 _LastPoint;
    //  更新动画
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

    //  获取范围半径
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
