using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillsTurretBehavior : MonoBehaviour
{
    public AudioSource shootAudio;

    public Color _DarkColor = Color.white;
    public float _AttackRadius = 3f;

    [SerializeField]
    private float _CoolingTime = 3f;//冷却时间
    [HideInInspector]
    public float _CoolingTimelReset;

    [SerializeField]
    private SpriteRenderer _Renderer = null;

    public Transform _Explosion;

    private GameObject _Anim;
    public Collider2D _Trigger;
    public float _AnimLength = 1.5f;

    private void Awake()
    {
        _CoolingTimelReset = _CoolingTime;
        _CoolingTime = 0f;

        _Trigger.enabled = true;
        _Anim = GetComponentInChildren<Animator>().gameObject;
        _Anim.SetActive(false);
        UpdateRadius(_AttackRadius);
    }

    private void Update()
    {
        CoolingUpdate();
    }

    public void UpdateRadius(float radius)
    {
        _Explosion.localScale = radius * Vector3.one;
    }

    //  释放技能
    private void ReleaseSkills()
    {
        shootAudio.Play();
        _Trigger.enabled = false;
        _Anim.SetActive(true);
        Invoke("ReleaseCompletely", _AnimLength);
    }

    //  技能完成释放
    private void ReleaseCompletely()
    {
        _CoolingTime = _CoolingTimelReset;
        _Anim.SetActive(false);
        _Trigger.enabled = true;
    }

    //  冷却更新
    private void CoolingUpdate()
    {
        if (_CoolingTime > 0) { _CoolingTime -= Time.deltaTime; }
        _Renderer.color = Color.Lerp(_DarkColor, Color.white, 1 - (Mathf.Max(0, _CoolingTime) / _CoolingTimelReset));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_Anim.activeInHierarchy && _CoolingTime <= 0)
        {
            if (collision.GetComponentInParent<EnemyHPBehavior>())
            {
                ReleaseSkills();
            }
        }
    }

}
