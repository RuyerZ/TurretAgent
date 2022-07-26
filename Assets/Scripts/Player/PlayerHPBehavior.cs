using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPBehavior : MonoBehaviour
{
    //public float maxHP = 10.0f;
    //private float currentHP;

    public int _TwinkleCount = 2;
    public Color _BeHitColor = Color.white;
    public SpriteRenderer _SR;

    private Coroutine _BeHitC;
    private Color _DefaultColor;

    void Start()
    {
        //currentHP = maxHP;
        //Debug.Assert(gameObject.GetComponent<BoxCollider2D>() != null);
        if (_SR == null)
        {
            _SR = GetComponent<SpriteRenderer>();
        }
        _DefaultColor = _SR.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Twinkle());
        }
    }

    public void TakeDamage(float value)
    {
        GameManager.sTheGlobalBehavior.ReduceBaseHP(value);
        if (_BeHitC != null)
        {
            StopCoroutine(_BeHitC);
            _BeHitC = null;
        }
        _BeHitC = StartCoroutine(Twinkle());
    }

    void CollisionCheck(GameObject o)
    {
        if (o.GetComponent<EnemyBulletBehavior>() != null)
        {
            EnemyBulletBehavior f = o.GetComponent<EnemyBulletBehavior>();
            TakeDamage(f.getDmg(gameObject));
            f.onHit(gameObject);
            //GameManager.sTheGlobalBehavior.ReduceBaseHP(f.getDmg(gameObject));

            //if (_BeHitC != null)
            //{
            //    StopCoroutine(_BeHitC);
            //    _BeHitC = null;
            //}
            //_BeHitC = StartCoroutine(Twinkle());
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        CollisionCheck(other.gameObject);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        CollisionCheck(other.gameObject);
    }

    //  ��˸
    private IEnumerator Twinkle()
    {
        //SetAlpha(1f);
        SetColor(Color.white);
        int count = 0;
        const float interval = 0.2f;
        while (count < _TwinkleCount)
        {
            //SetAlpha(0f);
            SetColor(_BeHitColor);
            yield return new WaitForSeconds(interval);
            //SetAlpha(1f);
            SetColor(_DefaultColor);
            yield return new WaitForSeconds(interval);
            count++;
        }
        yield return null;
    }

    private void SetAlpha(float alpha)
    {
        if (_SR == null)
        {
            return;
        }

        alpha = Mathf.Clamp01(alpha);

        Color color = _SR.color;
        color.a = alpha;
        _SR.color = color;
    }

    private void SetColor(Color color)
    {
        if (_SR == null)
        {
            return;
        }
        _SR.color = color;
    }

}