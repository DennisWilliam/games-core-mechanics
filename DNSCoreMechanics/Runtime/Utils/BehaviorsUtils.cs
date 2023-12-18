using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DNSCoreMechanics.Utils
{
    public class BehaviorsUtils 
    {
        public static IEnumerator doDash(
            bool canDash, bool isDashing, 
            Rigidbody2D rb, float moveDirectionX, 
            float moveDirectionY, float dashSpeed,
            float dashDuration, float dashCooldown)
        {
            canDash = false;
            isDashing = true;
            rb.velocity = new Vector2(moveDirectionX * dashSpeed, moveDirectionY * dashSpeed);
            yield return new WaitForSeconds(dashDuration);
            isDashing = false;
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }

        public static Vector2 getAxisDirection()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            return new Vector2(moveX, moveY);
        }

        public static Vector2 getNormalizedMoveDirection()
        {
            Vector2 axis = getAxisDirection();
            return new Vector2(axis.x, axis.y).normalized;
        }

        public static Vector2 getCameraMousePosition()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        public static Quaternion RotateAtFront(float movementX, float movementY)
        {
            int angle = 0;
            if (Input.GetAxis("Horizontal") < 0)
            {
                angle = -90;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                angle = 90;
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                angle = 0;
            }
            else if (Input.GetAxis("Vertical") > 0)
            {
                angle = 180;
            }
            return Quaternion.Euler(0, 0, angle);
        }

        public static bool IsGrounded(LayerMask groundLayer, Transform groundCheck_transform)
        {
            return Physics2D.CircleCast(groundCheck_transform.position, 0.1f, Vector2.down, 0.1f, groundLayer);
        }

        public static int ReceiveDammage(Collider2D collision, string targetTag, int health, int receivedDammage)
        {
            if (collision.CompareTag(targetTag))
            {
                health -= receivedDammage;
            }
            return health;
        }
    }
}

