using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMoveBehavior : MonoBehaviour
{
    bool valid = true;
    bool isCarried = false;

    public AudioSource pickUpAudio;
    public AudioSource dropAudio;

    void SetInvalid() 
    {
        if (!valid) return;

        SetColor(new Color(1f,0f,0f,0.7f));
        valid = false;
    }
    void SetValid() 
    {
        if (valid) return;

        SetColor(new Color(0f,1f,0f,0.7f));
        valid = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Path") {
            SetInvalid();
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Path") {
            SetInvalid();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Path") {
            SetValid();
        }
    }
    void SetColor(Color c)
    {
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        renderer.color = c;
    }
    public bool GetIsValid()
    {
        return valid;
    }
    public void SetIsCarried(bool flag)
    {
        if (flag) {
            SetColor(new Color(0f,1f,0f,0.7f));
            pickUpAudio.Play();
        } else {
            SetColor(new Color(1f,1f,1f,1f));
            dropAudio.Play();
        }

        isCarried = flag;
    }
}
