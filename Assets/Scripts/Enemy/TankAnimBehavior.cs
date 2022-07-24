using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAnimBehavior : MonoBehaviour {
    private Vector3 _LastPoint;
    bool facingRight = true;

    void Start() {
        _LastPoint = transform.position;
    }

    void Update() {
        if (transform.position.x < _LastPoint.x && facingRight) { 
            facingRight = false;
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;
        }
        else if (transform.position.x > _LastPoint.x && !facingRight){ 
            facingRight = true;
            Vector3 scale = transform.localScale;
            scale.x = -scale.x;
            transform.localScale = scale;
        }
        _LastPoint = transform.position;
    }
}