using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBehavior : MonoBehaviour {
    public string pathName;
    public float pathSpeed = 1.0f;
    public float baseDamage = 1.0f;
    private PathSystem pathSystem = null;
    private float pathDistance;
    private Animator _Anim;
    private Vector3 _LastPoint;
    private enum AnimState { Idle, Down, Up, Left, Right }
    public void Awake()
    {
        _Anim = GetComponentInChildren<Animator>();
        _LastPoint = transform.position;
    }
    public void Start()
    {
        pathSystem = GameManager.sTheGlobalBehavior.mPathSystem;
        GameManager.sTheGlobalBehavior.mEnemyManager.AddEnemy(gameObject);
        Debug.Assert(pathSystem != null);
        pathDistance = 0.0f;
        UpdatePosition();
    }
    public void Update()
    {
        UpdatePosition();
        UpdateAnim();
    }
    private void UpdatePosition()
    {
        //Debug.Assert(pathSystem != null);
        //Debug.Assert(pathName != null && pathSystem.PathExists(pathName));
        if ( Time.smoothDeltaTime == 0 ) return;
        pathDistance += pathSpeed * Time.smoothDeltaTime;
        Vector3 position = pathSystem.GetPositionFromPath(pathName, pathDistance);
        position.z = 0;
        if (pathSystem.IsPathEnd(pathName, pathDistance)) {
            GameManager.sTheGlobalBehavior.mEnemyManager.RemoveEnemy(gameObject);
            GameManager.sTheGlobalBehavior.ReduceBaseHP(baseDamage);
            Destroy(gameObject);
            return;
        }
        transform.position = position;
    }
    private void UpdateAnim()
    {
        if (_Anim == null || GameManager.sTheGlobalBehavior.isPaused) { return; }
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

    private float _TempSpeed;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //  减速塔
        if (other.CompareTag("DecelerateTurret"))
        {
            if (_DecelerateIE != null)
            {
                StopCoroutine(_DecelerateIE);
                pathSpeed = _TempSpeed;
            }
            _TempSpeed = pathSpeed;
            _DecelerateIE = StartCoroutine(IE_Decelerate(other.GetComponentInParent<DecelerateTurretBehavior>()));
        }
    }

    private Coroutine _DecelerateIE;
    //  减速处理协程
    private IEnumerator IE_Decelerate(DecelerateTurretBehavior turret)
    {
        if (turret == null) { yield break; }

        pathSpeed *= Mathf.Clamp01((1 - turret.DecelerateRatio));
        yield return new WaitForSeconds(turret.DecelerateTime);
        pathSpeed = _TempSpeed;
    }

    private void OnDestroy()
    {
        if (_DecelerateIE != null) { StopCoroutine(_DecelerateIE); }
    }

}