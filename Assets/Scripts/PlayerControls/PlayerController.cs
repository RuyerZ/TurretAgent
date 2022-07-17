using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    static private PlayerManager manager= null;
    public void setPlayerManager(PlayerManager m) {
        manager = m;
    }

    Vector2 moveDirection;
    Vector2 mousePoistion;

    private float aimAngle = 0f;    //aimAngle is passed down to AimController

    // Update is called once per frame
    void Update()
    {
        //movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        //action input
        handleActionInput();

        moveDirection = new Vector2(moveX, moveY).normalized;

        //get aimAngle for weapon
        mousePoistion = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 pos = transform.position;
        Vector2 aimDirection = mousePoistion - pos;
        
        aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;

        //set animation based on aimAngle
        animator.SetFloat("Horizontal", aimDirection.x);
        animator.SetFloat("Vertical", aimDirection.y);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);
    }

    private void handleActionInput()
    {
        if (Input.GetMouseButton(0))
        {
            manager.Fire();
        }   
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            manager.SetActiveItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            manager.SetActiveItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            manager.SetActiveItem(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            manager.SetActiveItem(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            manager.SetActiveItem(4);
        }
    }
    //for movement
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    public float GetAimAngle()
    {
        return aimAngle;
    }

}
