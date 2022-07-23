using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusTurretBehavior : TurretAttackBase
{
    public AudioSource shootAudio;

    public float _AttackRadius = 4f;
    public float _AttackDamage = 1f;
    public float _AttackInterval = 5f;
    public float _AttackIntervalReset;
    public void Start() {
        _AttackIntervalReset = _AttackInterval;
    }
    private void Update()
    {
        _AttackIntervalReset -= Time.smoothDeltaTime;
        if (_AttackIntervalReset <= 0)
        {
            _AttackIntervalReset = _AttackInterval;
            if (shootAudio != null)
                shootAudio.Play();
            List<GameObject> enemies = GameManager.sTheGlobalBehavior.mEnemyManager.GetEnemiesInRadius(transform.position ,_AttackRadius);
            foreach (GameObject enemy in enemies)
            {
                EnemyHPBehavior t = enemy.GetComponent<EnemyHPBehavior>();
                if (t!=null)
                    t.DamageEnemy(_AttackDamage);
            }
        }
    }
}
