using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DNSCoreMechanics.Intefaces
{
    public interface IEntityBehavior
    {
        void InitializeEntityBehaviorRequiredConfigs(float movementSpeed, float jumpPower);
        void Move(Transform entityTransform, Animator anim, float movementSpeed, GameObject lookAtDirection);
        void Jump();
        void Shoot();
        void MeleeAtack();
        void Dash(bool isDashing, bool canDash);
        void Respawn();
        void hasCollision();
    }
}

