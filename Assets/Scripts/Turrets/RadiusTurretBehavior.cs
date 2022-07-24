using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusTurretBehavior : TurretAttackBase
{

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
