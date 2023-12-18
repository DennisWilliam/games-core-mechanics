using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DNSCoreMechanics.Utils
{
    public class AIUtils
    {
        /// <summary>Method used to rotate current entity to look at the target.</summary>
        /// <param name="target">Target GameObject </param>
        public static void RotateFowardsAnotherEntity(GameObject target, Transform currentEntityTransform)
        {
            float angle = Mathf.Atan2(
                target.transform.position.y - currentEntityTransform.position.y,
                target.transform.position.x - currentEntityTransform.position.x
                ) * Mathf.Rad2Deg;

            currentEntityTransform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        }
    }
}

