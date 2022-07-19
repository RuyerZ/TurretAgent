using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurretBehavior : MonoBehaviour
{
    private bool isCarried = false;
    private const float radius = 0.2f;
    private const float pickUpRadius = 0.4f;
    public GameObject turret;

    // Update is called once per frame
    void Update()
    {
        if (!isCarried && Input.GetKeyDown(KeyCode.E)) {
            handleCarryTurret();
        }
        else if (isCarried) {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButton(0)) {
                handleDropTurret();
            } else {
                handleMoveTurret();
            }
        }
        if (isCarried) {
            Vector2 mousePoistion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 playerPosition = transform.position;
            playerPosition.y += 0.2f;

            Vector2 playerToMouseDir = (mousePoistion - playerPosition).normalized;

            Vector3 pos = playerPosition + (playerToMouseDir * radius);

            if (mousePoistion.y > playerPosition.y) {
                pos.z = 0.001f;
            } else {
                pos.z = -0.001f;
            }

            turret.transform.position = pos;

        }
    }
    void handleCarryTurret()
    {
        Vector2 playerPosition = transform.position;
        Vector2 turretPosition = turret.transform.position;

        if (Vector2.Distance(playerPosition,turretPosition) > pickUpRadius) {
            Debug.Log("too far!");
            return;
        }

        turret.GetComponent<TurretShootBehavior>().enabled = false;
        gameObject.GetComponent<PlayerItemBehavior>().Deactivate();
        isCarried = true;
    }
    void handleDropTurret()
    {
        //CHECK DROP VALIDITY

        turret.GetComponent<TurretShootBehavior>().enabled = true;
        gameObject.GetComponent<PlayerItemBehavior>().Activate();
        
        isCarried = false;
    }
    void handleMoveTurret()
    {
        Vector2 mousePoistion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = transform.position;
        Vector2 playerToMouseDir = (mousePoistion - playerPosition).normalized;
        Vector3 pos = playerPosition + playerToMouseDir * radius;

        if (mousePoistion.y > playerPosition.y) {
            pos.z = 0.001f;
        } else {
            pos.z = -0.001f;
        }

        turret.transform.position = pos;
    }
}
