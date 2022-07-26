using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillsTurretBehavior : TurretAttackBase
{

    public Color _DarkColor = Color.white;

    public float _CoolingTime = 3f;//��ȴʱ��
    [HideInInspector]
    public float _CoolingTimelReset;

    public SpriteRenderer _Renderer = null;

    public Transform _Explosion;

    private GameObject _Anim;
    public Collider2D _Trigger;
    public float _AnimLength = 1.5f;

    private bool _BePicked;

    private void Awake()
    {
        _CoolingTimelReset = _CoolingTime;
        _CoolingTime = 0f;

        _Trigger.enabled = true;
        _Anim = GetComponentInChildren<Animator>().gameObject;
        _Anim.SetActive(false);
        UpdateRadius(_AttackRadius);
    }

    private void OnEnable()
    {
        SetPickState(false);
    }

    private void OnDisable()
    {
        SetPickState(true);
    }

    private void SetPickState(bool pick)
    {
        _BePicked = pick;
        //_Trigger.enabled = !pick;
        _Trigger.gameObject.SetActive(!pick);
    }

    private void Update()
    {
        CoolingUpdate();
    }

    public void UpdateRadius(float radius)
    {
        _Explosion.localScale = radius * Vector3.one;
    }

    //  �ͷż���
    private void ReleaseSkills()
    {
        shootAudio.Play();
        _Trigger.enabled = false;
        _Anim.SetActive(true);
        Invoke("ReleaseCompletely", _AnimLength);
    }

    //  ��������ͷ�??
    private void ReleaseCompletely()
    {
        _CoolingTime = _CoolingTimelReset;
        _Anim.SetActive(false);
        _Trigger.enabled = true;
    }

    //  ��ȴ����
    private void CoolingUpdate()
    {

        if (_BePicked)
            return;
        if (_CoolingTime > 0)
        {
            _CoolingTime -= Time.deltaTime;
        }
        _Renderer.color = Color.Lerp(_DarkColor, Color.white, 1 - (Mathf.Max(0, _CoolingTime) / _CoolingTimelReset));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_BePicked)
            return;

        if (!_Anim.activeInHierarchy && _CoolingTime <= 0)
        {
            if (collision.GetComponentInParent<EnemyHPBehavior>())
            {
                ReleaseSkills();
            }
        }
    }
}