using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNSCoreMechanics.Behaviors.Entities;
using DNSCoreMechanics.Intefaces;
using DNSCoreMechanics.ScriptObjects;
using DNSCoreMechanics.Utils;

public class EntityAIPlataform2D : EntityBehavior2D<EntityPlataform2D>, IEntityAI
{
    [Header("AI Configs")]
    public EntityBaseSO entityScriptObject;
    protected float distanceToTarget;
    public float height;
    [SerializeField] GameObject target;

    [Header("Chase Settings")]
    [SerializeField] float speed;
    [SerializeField] float distance;
    [SerializeField] float distanceBetween;

    public void MoveFowardToEntity(float entityDistance, GameObject target)
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

    public void RotateFowardsAnotherEntity()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BehaviorsUtils.ReceiveDammage(collision, collision.tag, health, 20);
    }

    public void Chase()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, EBInstance.movementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}
