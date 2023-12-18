using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DNSCoreMechanics.Intefaces
{
    public interface IEntityAI 
    {
        void RotateFowardsAnotherEntity();
        void MoveFowardToEntity(float entityDistance, GameObject target);
        //void OnTriggerEnter2D(Collider2D collision, string targetToCollideTag, int dammageOnCollide);
        void Chase();
    }
}

