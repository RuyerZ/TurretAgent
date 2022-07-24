using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusTurretBehavior : SkillsTurretBehavior
{
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
