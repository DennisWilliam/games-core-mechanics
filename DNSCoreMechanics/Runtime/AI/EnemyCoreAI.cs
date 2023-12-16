using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNSCoreMechanics.AI;
using DNSCoreMechanics.Weapons;


public class EnemyCoreAI : EntityAI
{
    GameObject player;
    GameObject bulletParent;

    [Header("Chase Settings")]
    [SerializeField] float speed;
    [SerializeField] float distance;
    [SerializeField] float distanceBetween;


    /// <summary>Method used to initialize required configs, this method should be called in start method.</summary>
    /// <param name="playerName">Get player name</param>
    protected void initializeEnemyCoreAIRequiredConfigs(string playerTagName, string bulletParentTagName)
    {
        this.player = GameObject.Find(playerTagName);
        this.bulletParent = GameObject.Find(bulletParentTagName);
        //this.player = GameObject.FindWithTag(playerTagName);
        //this.bulletParent = GameObject.FindWithTag(bulletParentTagName);

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (entityScriptObject != null)
        {
            if (entityScriptObject && entityScriptObject.sprite != null)
            {
                spriteRenderer.sprite = entityScriptObject.sprite;
            }
            health = entityScriptObject.healthMax;
            height = Random.Range(entityScriptObject.height - 0.5f, entityScriptObject.height + 0.5f);
        }
        //distancePlayer = 10;
    }

    // Update is called once per frame
    protected void UpdateBehaviors()
    {
        ManageHealth();
        AttackTarget();
        RotateFowardsAnotherEntity(player);
        MoveFowardToEntity(entityScriptObject.distanceToEntities, player);
    }

    protected void ManageHealth()
    {
        spriteRenderer.color = new Color(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            Mathf.Pow(health / entityScriptObject.healthMax, 0.7f)
           );

        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

    protected void AttackTarget()
    {
        if (distanceToTarget <= entityScriptObject.distanceToEntities && canShoot)
        {
            UseWeapon(gameObject);
        }
    }

    protected void ChasePlayer()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}
