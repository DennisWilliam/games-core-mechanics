using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DNSCoreMechanics.ScriptObjects
{
    [CreateAssetMenu(fileName = "EntityBaseSO", menuName = "Scriptable Objects/EntityBaseSO")]
    public class EntityBaseSO : ScriptableObject
    {
        public Sprite sprite;
        public float healthMax;
        public bool hasDammage;
        public float height;
        public float distanceToEntities;
    }
}
