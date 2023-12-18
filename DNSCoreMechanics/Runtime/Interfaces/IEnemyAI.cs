using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DNSCoreMechanics.Intefaces
{
    public interface IEnemyAI 
    {
        void BehaviorOnUpdate();
        void RequiredConfigs();
        void ManageHealth();
        void AttackTarget();
    }
}
