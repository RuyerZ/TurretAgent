using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCarried : MonoBehaviour
{
    private bool isCarried = false;
    private const float radius = 3f;
    public PlayerController controller;

    // Update is called once per frame
    void Update()
    {
        //not the most efficient as there values are calculated in controller
        if (isCarried) {
            Vector3 mousePoistion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 playerPosition = controller.transform.position;
            playerPosition.y += 0.2f;

            Vector3 playerToMouseDir = (mousePoistion - playerPosition).normalized;

            Vector3 pos = playerPosition + playerToMouseDir * radius;

            if (mousePoistion.y > playerPosition.y) {
                pos.z = 0.001f;
            } else {
                pos.z = -0.001f;
            }

            transform.position = pos;

        }
    }
    public void SetIsCarried(bool f)
    {
        isCarried = f;
    }
}
