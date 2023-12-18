using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNSCoreMechanics.Intefaces;
using DNSCoreMechanics.Utils;

namespace DNSCoreMechanics.TopDown2D.Entities
{

}

public class EntityTopDown2D: IEntityBehavior
{
   // [Header("Moving Settings TopDown 2D")]
   // [SerializeField] public float movementSpeed;
   // [SerializeField] public Rigidbody2D rb; //REPETITION
   // [SerializeField] public GameObject lookAtDirection;
   // public Transform entityTransform;

   // [Header("Dash Settings")]
   // [SerializeField] public float dashSpeed = 10f; //REPETITION
   // [SerializeField] public float dashDuration = 1f; //REPETITION
   // [SerializeField] public float dashCooldown = 1f; //REPETITION 
   // bool isDashing;
   // bool canDash;
   // Vector2 moveDirection;
   // Vector2 mousePosition;

    //[Header("Animation Settings 2D")]
   // [SerializeField] protected Animator anim;


    //REPETITION
    public void Dash(bool isDashing, bool canDash, Rigidbody2D rb, float dashSpeed, float dashDuration, int dashCooldown)
    {
        if (isDashing)
        {
            return;
        }
        Vector2 moveDirection = BehaviorsUtils.getNormalizedMoveDirection();
        Vector2 mousePosition = BehaviorsUtils.getCameraMousePosition();
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            //StartCoroutine(BehaviorsUtils.doDash(canDash, isDashing, rb, moveDirection.x, moveDirection.y, dashSpeed, dashDuration, dashCooldown));
        }
    }

    public void hasCollision()
    {
        throw new System.NotImplementedException();
    }

    public void InitializeEntityBehaviorRequiredConfigs(float movementSpeed, float jumpPower)
    {
        //this.movementSpeed = movementSpeed;
        //canDash = true;
    }

    public void Jump()
    {
        throw new System.NotImplementedException();
    }

    public void MeleeAtack()
    {
        throw new System.NotImplementedException();
    }

    public void Move(Transform entityTransform, Animator anim, float movementSpeed, GameObject lookAtDirection)
    {
        Vector3 movement = BehaviorsUtils.getNormalizedMoveDirection();
        Debug.Log(movement);
        if (anim != null)
        {
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
            anim.SetFloat("speed", movement.magnitude);
        }

        Debug.Log(lookAtDirection);

        entityTransform.position = entityTransform.position + movement * movementSpeed * Time.deltaTime;
        lookAtDirection.transform.rotation = BehaviorsUtils.RotateAtFront(movement.x, movement.y);
    }

    public void MoveWithRigidBody(Rigidbody2D rb, float movementSpeed)
    {
        Vector2 moveDirection = BehaviorsUtils.getNormalizedMoveDirection();
        Vector2 mousePosition = BehaviorsUtils.getCameraMousePosition();
        rb.velocity = new Vector2(moveDirection.x * movementSpeed, moveDirection.y * movementSpeed);
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    public void Respawn()
    {
        throw new System.NotImplementedException();
    }

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }
}
