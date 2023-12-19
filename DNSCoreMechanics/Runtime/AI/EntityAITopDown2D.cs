using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNSCoreMechanics.Behaviors.Entities;
using DNSCoreMechanics.ScriptObjects;
using DNSCoreMechanics.Utils;
using DNSCoreMechanics.Intefaces;

public class EntityAITopDown2D : EntityBehavior2D<EntityTopDown2D>, IEntityAI
{
    [Header("AI Settings (Required)")]
    [SerializeField] public EntityBaseSO entityScriptObject;
    [SerializeField] GameObject target;
    protected float distanceToTarget;

    [Header("Chase Settings (Required only if chase is used)")]
    [SerializeField] protected float distanceBetween;
    protected float distanceToChase;

    public void MoveFowardToEntity(float entityDistance, GameObject target)
    {
        throw new System.NotImplementedException();
    }

    public void RotateFowardsAnotherEntity()
    {
        AIUtils.RotateFowardsAnotherEntity(target, gameObject.transform );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BehaviorsUtils.ReceiveDammage(collision, collision.tag, health, 20);
    }

    public void Chase()
    {
        distanceToChase = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        if (distanceToChase < distanceBetween)
        {
           transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, movementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}
