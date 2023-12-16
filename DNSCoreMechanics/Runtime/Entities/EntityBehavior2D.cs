using System.Collections;
using UnityEngine;
using DNSCoreMechanics.Weapons;


namespace DNSCoreMechanics.Behaviors.Entities
{
    public class EntityBehavior2D:MonoBehaviour
    {
        [Header("Moving Settings")]
        [SerializeField] protected bool isAIControl;
        [SerializeField] public float movementSpeed;
        public float jumpPower;
        [SerializeField] protected Rigidbody2D rigidbody;

        [Header("Moving Plataform 2D")]
        [SerializeField] protected LayerMask groundLayer;
        [SerializeField] protected Transform groundCheck_transform;

        [Header("Weapons Settings")]
        [SerializeField] protected GameObject bullet_prefab;
        [SerializeField] protected GameObject bullet_parent;
        [SerializeField] protected Transform initialBulletPos_transform;
        protected WeaponBehavior2D weaponBehavior;
        protected bool canShoot = true;

        [Header("Moving Settings TopDown 2D")]
        [SerializeField] protected Animator anim;
        [SerializeField] protected GameObject lookAtDirection;

        [Header("Dash Settings")]
        [SerializeField] float dashSpeed = 10f;
        [SerializeField] float dashDuration = 1f;
        [SerializeField] float dashCooldown = 1f;
        bool isDashing;
        bool canDash;
        Vector2 moveDirection;
        Vector2 mousePosition;


        /*
        ENTITY MOVEMENTS
        */

        /// <summary>Method used to initialize entity movement and jump force, this method should be called in start method.</summary>
        /// <param name="movementSpeed">Add velocity to entity movement.</param>
        /// <param name="jumpPower">Add force to entity jump.</param>
        protected void initializeEntityBehaviorRequiredConfigs(float movementSpeed, float jumpPower )
        {
            //TODO não pode utilizar o new, dever ser adicionado no componente
            weaponBehavior = new WeaponBehavior2D();
            this.movementSpeed = movementSpeed;
            this.jumpPower = jumpPower;
            canDash = true;
        }

        protected void ExecuteDash()
        {
            if (isDashing)
            {
                return;
            }
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector2(moveX, moveY).normalized;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetKeyDown(KeyCode.Space) && canDash)
            {
                StartCoroutine(Dash());
            }
        }

        private IEnumerator Dash()
        {
            canDash = false;
            isDashing = true;
            rigidbody.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
            yield return new WaitForSeconds(dashDuration);
            isDashing = false;
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }

        /// <summary>Method used to move entity, this method should be called in update method.</summary>
        protected void Move()
        {
            float horizontal = Input.GetAxisRaw("Horizontal") * movementSpeed;
            rigidbody.velocity = new Vector2(horizontal, rigidbody.velocity.y);

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpPower);
            }
        }

        /// <summary>Method used to move entity, this method should be called in update method.</summary>
        protected void topDownMove2D()
        {
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (anim != null)
            {
                anim.SetFloat("Horizontal", movement.x);
                anim.SetFloat("Vertical", movement.y);
                anim.SetFloat("speed", movement.magnitude);
            }
            movement.Normalize();

            transform.position = transform.position + movement * movementSpeed * Time.deltaTime;

            LookAtFront(movement.x, movement.y);
        }

        protected void topDownMove2DWithRigidBody()
        {
            rigidbody.velocity = new Vector2(moveDirection.x * movementSpeed, moveDirection.y * movementSpeed);

            Vector2 aimDirection = mousePosition - rigidbody.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rigidbody.rotation = aimAngle;
        }

        protected void LookAtFront(float movementX, float movementY)
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
            lookAtDirection.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        /// <summary>Method used to check if there is any collision with object with 'ground' tag.</summary>
        bool IsGrounded()
        {
            return Physics2D.CircleCast(groundCheck_transform.position, 0.1f, Vector2.down, 0.1f, groundLayer);
        }

        /*
        ENTITY USING WEAPONS
        */

        //Coroutine that block mouse click after clicking for 1 second
        protected IEnumerator ShootingCoolDown(float cooldownTime)
        {
            canShoot = false;
            yield return new WaitForSeconds(cooldownTime);
            canShoot = true;
        }

        protected void UseWeapon(GameObject weapon_gameObject)
        {
            
            if (Input.GetMouseButton(0) && canShoot && !isAIControl)
            {
                StartCoroutine(ShootingCoolDown(0.5f));
                weaponBehavior.OnUseGun(weapon_gameObject, bullet_prefab, bullet_parent, initialBulletPos_transform,10, false);
            } else if (canShoot && isAIControl)
            {
                StartCoroutine(ShootingCoolDown(0.5f));
                weaponBehavior.OnUseGun(weapon_gameObject, bullet_prefab, bullet_parent, initialBulletPos_transform, 10, true);
            }
        }

    }

}

