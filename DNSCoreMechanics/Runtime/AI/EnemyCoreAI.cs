using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNSCoreMechanics.AI;
using DNSCoreMechanics.Weapons;


public class EnemyCoreAI : EntityAI
{
    GameObject player;
    GameObject bulletParent;
    

    /// <summary>Method used to initialize required configs, this method should be called in start method.</summary>
    /// <param name="playerName">Get player name</param>
    protected void initializeEnemyCoreAIRequiredConfigs(string playerTagName, string bulletParentTagName)
    {
        this.player = GameObject.Find(playerTagName);
        this.bulletParent = GameObject.Find(bulletParentTagName);
        //this.player = GameObject.FindWithTag(playerTagName);
        //this.bulletParent = GameObject.FindWithTag(bulletParentTagName);

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (entityScriptObject.sprite != null)
        {
            spriteRenderer.sprite = entityScriptObject.sprite;
        }

        health = entityScriptObject.healthMax;
        height = Random.Range(entityScriptObject.height - 0.5f, entityScriptObject.height + 0.5f);
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
}
