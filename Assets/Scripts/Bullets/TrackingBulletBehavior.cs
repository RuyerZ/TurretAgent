using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBulletBehavior : EnemyBulletBehavior
{
    public float followFactor = 0.06f;
    public float lifetime = 5f;

    public void Update()
    {
        if (GameManager.sTheGlobalBehavior.isPaused) return;

        base.Update();
        // Check lifetime
        lifetime -= Time.smoothDeltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
        // Follow the player
        PlayerMoveBehavior player = GameManager.sTheGlobalBehavior.mHero;
        if (player != null)
        {
            Vector3 target = player.transform.position;
            Vector2 direction = target - transform.position;
            
            Vector2 newDirection = Vector2.Lerp(transform.up, direction, followFactor);
            float angularChangeInDegrees = Vector2.SignedAngle(transform.up, direction);

            //transform.rotation = Quaternion.LookRotation(Vector3.forward, newDirection);
            var body = GetComponent<Rigidbody2D>();
            body.AddForce(direction.normalized * followFactor, ForceMode2D.Impulse);
            body.velocity = Vector2.ClampMagnitude(body.velocity, fireforce);
        }
    }
}
