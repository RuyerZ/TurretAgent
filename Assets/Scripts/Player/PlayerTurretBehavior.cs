using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurretBehavior : MonoBehaviour
{
    private bool isCarried = false;
    private const float radius = 0.8f;
    private const float pickUpRadius = 0.8f;
    private GameObject turret;

    void Start()
    {
        turret = GameManager.sTheGlobalBehavior.mFriendManager.GetClosestTurret(transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        if (!isCarried)
            turret = GameManager.sTheGlobalBehavior.mFriendManager.GetClosestTurret(transform.position);

        if (!isCarried && Input.GetKeyDown(KeyCode.F))
        {
            handleUpgradeTurret();
        }
        if (!isCarried && Input.GetKeyDown(KeyCode.E))
        {
            handleCarryTurret();
        }
        else if (isCarried)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                handleDropTurret();
            }
            else
            {
                handleMoveTurret();
            }
        }
        Vector2 playerPosition = transform.position;

        if (isCarried)
        {
            Vector2 mousePoistion = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //playerPosition.y += 1f;

            Vector2 playerToMouseDir = (mousePoistion - playerPosition).normalized;

            Vector3 pos = playerPosition + (playerToMouseDir * radius);

            if (mousePoistion.y > playerPosition.y)
            {
                pos.z = 0.001f;
            }
            else
            {
                pos.z = -0.001f;
            }

            turret.transform.position = pos;

        }

        playerPosition = transform.position;
        Vector2 turretPosition = turret.transform.position;

        if (Vector2.Distance(playerPosition, turretPosition) <= pickUpRadius && !isCarried)
        {
            turret.transform.Find("glow").gameObject.SetActive(true);
        }
        else
        {
            turret.transform.Find("glow").gameObject.SetActive(false);
        }

    }
    void handleCarryTurret()
    {
        Vector2 playerPosition = transform.position;
        Vector2 turretPosition = turret.transform.position;

        if (Vector2.Distance(playerPosition, turretPosition) > pickUpRadius)
        {
            Debug.Log("too far!");
            return;
        }
        turret.GetComponent<TurretAttackBase>().enabled = false;
        gameObject.GetComponent<PlayerItemBehavior>().Deactivate();
        isCarried = true;
        turret.GetComponent<TurretMoveBehavior>().SetIsCarried(true);
    }
    void handleDropTurret()
    {
        //CHECK DROP VALIDITY
        bool valid = turret.GetComponent<TurretMoveBehavior>().GetIsValid();
        if (!valid)
            return;

        turret.GetComponent<TurretAttackBase>().enabled = true;
        gameObject.GetComponent<PlayerItemBehavior>().Activate();

        isCarried = false;
        turret.GetComponent<TurretMoveBehavior>().SetIsCarried(false);
    }
    void handleMoveTurret()
    {
        Vector2 mousePoistion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = transform.position;
        Vector2 playerToMouseDir = (mousePoistion - playerPosition).normalized;
        Vector3 pos = playerPosition + playerToMouseDir * radius;

        if (mousePoistion.y > playerPosition.y)
        {
            pos.z = 0.001f;
        }
        else
        {
            pos.z = -0.001f;
        }

        turret.transform.position = pos;
    }
    void handleUpgradeTurret()
    {
        GameManager.sTheGlobalBehavior.Pause();
        GameManager.sTheGlobalBehavior.UpgradeUI.GetComponent<UpgradeUI>().SetTurret(turret);
        GameManager.sTheGlobalBehavior.UpgradeUI.SetActive(true);

    }
}
