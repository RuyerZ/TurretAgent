using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {

        PlayerController p = gameObject.GetComponentInParent<PlayerController>();

        float aimAngle = p.GetAimAngle();

        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }
}
