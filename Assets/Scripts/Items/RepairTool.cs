using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairTool : ItemBase
{
    private TurretHPBehavior turretHP;
    public GameObject player;
    public float repairRate = 1f;
    public float repairTime = 0.5f;
    private float timeStamp = 0;
    private float repairRadius = 0.2f;
    private PlayerMoveBehavior moveBehavior;
    private PlayerTurretBehavior turretBehavior;
    public bool isRepairing = false;

    //animation
    private float speed = 12f;
    private float maxRotation = 30f;

    void Awake()
    {
        moveBehavior = player.GetComponent<PlayerMoveBehavior>();
        turretBehavior = player.GetComponent<PlayerTurretBehavior>();
        turretHP = GameManager.sTheGlobalBehavior.mFriendManager.GetClosestTurret(transform.position).GetComponent<TurretHPBehavior>();
        Debug.Assert(moveBehavior);
        Debug.Assert(turretBehavior);
        //temp
        itemCount = 100;
    }
    void Update()
    {
        turretHP = GameManager.sTheGlobalBehavior.mFriendManager.GetClosestTurret(transform.position).GetComponent<TurretHPBehavior>();

        if (moveBehavior.GetSpeed() > 0.01f && isRepairing) {
            StopRepair();
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if (isRepairing) {
            Repair();
            transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed));
        }
    }

    public override void Fire() 
    {
        if (isRepairing) return;

        Vector2 toolPosition = firePoint.position;
        Vector2 turretPosition = turretHP.transform.position;

        if (Vector2.Distance(toolPosition,turretPosition) > repairRadius) {
            return;
        }
        if (itemCount <= 0) return;
        turretBehavior.enabled = false;
        isRepairing = true;
        moveBehavior.isRepairing = true;
        
    }
    void Repair()
    {
        if (Time.time < timeStamp) return;
        if (itemCount <= 0) {
            StopRepair();
            return;
        } 
        if (turretHP.GetCurrentHP() == turretHP.maxHP) {
            return;
        }

        turretHP.Repair(repairRate);
        itemCount--;

        timeStamp = Time.time + repairTime;
    }
    void StopRepair()
    {
        turretBehavior.enabled = true;
        isRepairing = false;
        moveBehavior.isRepairing = false;
    }
}
