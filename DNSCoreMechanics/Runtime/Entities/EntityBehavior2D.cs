using System.Collections;
using UnityEngine;
using DNSCoreMechanics.Weapons;


namespace DNSCoreMechanics.Behaviors.Entities
{
    public class EntityBehavior2D:MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] protected bool isAIControl;
        public float movementSpeed;
        public float jumpPower;
        [SerializeField] protected LayerMask groundLayer;
        [SerializeField] protected Rigidbody2D rigidbody;
        [SerializeField] protected Transform groundCheck_transform;
        [SerializeField] protected GameObject bullet_prefab;
        [SerializeField] protected GameObject bullet_parent;
        [SerializeField] protected Transform initialBulletPos_transform;
        protected WeaponBehavior2D weaponBehavior;
        protected bool canShoot = true;

        /*
        ENTITY MOVEMENTS
        */

        /// <summary>Method used to initialize entity movement and jump force, this method should be called in start method.</summary>
        /// <param name="movementSpeed">Add velocity to entity movement.</param>
        /// <param name="jumpPower">Add force to entity jump.</param>
        protected void initializeEntityBehaviorRequiredConfigs(float movementSpeed, float jumpPower )
        {
            weaponBehavior = new WeaponBehavior2D();
            this.movementSpeed = movementSpeed;
            this.jumpPower = jumpPower;
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

