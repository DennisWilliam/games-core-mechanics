using System.Collections;
using UnityEngine;
using DNSCoreMechanics.Weapons;
using DNSCoreMechanics.Intefaces;


namespace DNSCoreMechanics.Behaviors.Entities
{
    public class EntityBehavior2D<EB> : MonoBehaviour, IEntityBehavior where EB : IEntityBehavior
    {
        protected EB EBInstance;
        [Header("Behavior Settings (Required)")]
        [SerializeField] protected bool isAIControl;
        [SerializeField] protected int health;
        [SerializeField] protected int hasShield;
        protected WeaponBehavior2D weaponBehavior;

        [Header("Moving Settings TopDown 2D")]
        [SerializeField] public float movementSpeed;
        [SerializeField] public Rigidbody2D rb; //REPETITION
        [SerializeField] public GameObject lookAtDirection;
        public Transform entityTransform;

        [Header("Dash Settings")]
        [SerializeField] public float dashSpeed = 10f; //REPETITION
        [SerializeField] public float dashDuration = 1f; //REPETITION
        [SerializeField] public float dashCooldown = 1f; //REPETITION 
        protected bool isDashing;
        protected bool canDash;

        [Header("Animation Settings 2D")]
        [SerializeField] protected Animator anim;

        public void Dash(bool isDashing, bool canDash)
        {
            EBInstance.Dash( isDashing, canDash);
        }

        public void hasCollision()
        {
            EBInstance.hasCollision();
        }

        public void InitializeEntityBehaviorRequiredConfigs(float movementSpeed, float jumpPower)
        {
            EBInstance.InitializeEntityBehaviorRequiredConfigs(movementSpeed, jumpPower);
        }

        public void Jump()
        {
            EBInstance.Jump();
        }

        public void MeleeAtack()
        {
            EBInstance.MeleeAtack();
        }

        public void Move(Transform entityTransform, Animator anim, float movementSpeed, GameObject lookAtDirection)
        {
            EBInstance.Move(entityTransform, anim, movementSpeed, lookAtDirection);
        }

        public void Respawn()
        {
            EBInstance.Respawn();
        }

        public void Shoot()
        {
            EBInstance.Shoot();
        }

        
    }

}

