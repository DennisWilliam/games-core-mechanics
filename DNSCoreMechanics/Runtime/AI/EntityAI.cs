using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNSCoreMechanics.ScriptObjects;
using DNSCoreMechanics.Behaviors.Entities;


namespace DNSCoreMechanics.AI
{
    public class EntityAI : EntityBehavior2D
    {
        [Header("AI Configs")]
        public EntityBaseSO entityScriptObject;
        public float health;
        public float height;
        protected SpriteRenderer spriteRenderer;
        protected float distanceToTarget;

        /// <summary>Method used to rotate current entity to look at the target.</summary>
        /// <param name="target">Target GameObject </param>
        protected void RotateFowardsAnotherEntity(GameObject target)
        {
            float angle = Mathf.Atan2(
                target.transform.position.y - transform.position.y,
                target.transform.position.x - transform.position.x
                ) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        }

        /// <summary>Method used to foward current entity to target.</summary>
        /// <param name="entityDistance">Distance that must be maintained from the target.</param>
        /// <param name="distanceToTarget">Current distance from the target that entity have</param>
        /// <param name="target">Target GameObject </param>
        protected void MoveFowardToEntity(float entityDistance , GameObject target )
        {
            distanceToTarget = Vector2.Distance(transform.position, target.transform.position);

            if (distanceToTarget > entityDistance || transform.position.y > height)
            {

                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y - Mathf.Pow((distanceToTarget - height + 2) * 0.017f, 3),
                    0
                    );
            }
            else if (transform.position.y < height - 1)
            {
                transform.position = new Vector3(
                   transform.position.x,
                   transform.position.y + Mathf.Pow((distanceToTarget - height + 2) * 0.017f, 3),
                   0
                   );
            }

            if (target.transform.position.x > transform.position.x)
            {
                transform.position = new Vector3(
                   transform.position.x,
                   transform.position.y - Mathf.Pow((distanceToTarget - height + 2) * 0.017f, 3),
                   0
                   );
            }
            else
            {
                transform.position = new Vector3(
                   transform.position.x,
                   transform.position.y + Mathf.Pow((distanceToTarget - height + 2) * 0.017f, 3),
                   0
                   );
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet"))
            {
                health -= 20;
            }
        }

        /// <summary>Method used to identify collisions.</summary>
        /// <param name="collision">Collider2D component.</param>
        /// <param name="targetToCollideTag">Target tag that can trigger this method </param>
        /// <param name="dammageOnCollide">Dammage value that will be subtract from the entity health.</param>
        private void OnTriggerEnter2D(Collider2D collision, string targetToCollideTag, int dammageOnCollide)
        {
            if (collision.CompareTag(targetToCollideTag))
            {
                health -= dammageOnCollide;
            }
        }


    }
}

