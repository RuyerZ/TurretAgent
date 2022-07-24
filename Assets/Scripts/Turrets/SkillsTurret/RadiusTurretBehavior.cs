using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusTurretBehavior : SkillsTurretBehavior
{
    [SerializeField]
    private float _AttackDamage = 1f;

    public float AttackDamage
    {
        get
        {
            return _AttackDamage;
        }
        set
        {
            _AttackDamage = value;
        }
    }
   
}
