using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//EMPTY CLASS WRAPPER FOR DIFFERENT ATTACK BEHAVIORS

public class TurretAttackBase : MonoBehaviour
{
    public AudioSource shootAudio;

    public float _AttackRadius;
    public float _AttackDamage;
    public float _AttackInterval;
    protected float _AttackIntervalReset;    

    public float GetAttackRadius()
    {
        return _AttackRadius;
    }
    public float GetAttackDamage()
    {
        return _AttackDamage;
    }
    public float GetAttackInterval()
    {
        return _AttackInterval;
    }
}
