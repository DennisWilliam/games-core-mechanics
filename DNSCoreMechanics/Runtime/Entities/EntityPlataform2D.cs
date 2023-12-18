using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNSCoreMechanics.Intefaces;
using DNSCoreMechanics.Utils;

public class EntityPlataform2D : MonoBehaviour, IEntityBehavior
{
    [Header("Moving Settings")]
    [SerializeField] public float movementSpeed;
    [SerializeField] public float jumpPower;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public Transform groundCheck_transform;
    [SerializeField] public GameObject lookAtDirection; //REPETITION
    Transform entityTransform;

    [Header("Dash Settings")]
    [SerializeField] public float dashSpeed = 10f; //REPETITION
    [SerializeField] public float dashDuration = 1f; //REPETITION
    [SerializeField] public float dashCooldown = 1f; //REPETITION 
    bool isDashing;
    bool canDash;
    Vector2 moveDirection;
    Vector2 mousePosition;

    [Header("Animation Settings 2D")]
    [SerializeField] protected Animator anim;//REPETITION

    public void InitializeEntityBehaviorRequiredConfigs(float movementSpeed, float jumpPower)
    {
        //TODO não pode utilizar o new, dever ser adicionado no componente
        //weaponBehavior = new WeaponBehavior2D();
        this.movementSpeed = movementSpeed;
        this.jumpPower = jumpPower;
        canDash = true;
    }

    public void Dash(bool isDashing, bool canDash, Rigidbody2D rb, float dashSpeed, float dashDuration, int dashCooldown)
    {
        if (isDashing)
        {
            return;
        }
        moveDirection = BehaviorsUtils.getNormalizedMoveDirection();
        mousePosition = BehaviorsUtils.getCameraMousePosition();
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(BehaviorsUtils.doDash(canDash, isDashing, rb, moveDirection.x, moveDirection.y, dashSpeed, dashDuration, dashCooldown));
        }
    }

    public void hasCollision()
    {
        throw new System.NotImplementedException();
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
        float horizontal = Input.GetAxisRaw("Horizontal") * movementSpeed;
        rb.velocity = new Vector2(horizontal, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.CircleCast(groundCheck_transform.position, 0.1f, Vector2.down, 0.1f, groundLayer);
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
