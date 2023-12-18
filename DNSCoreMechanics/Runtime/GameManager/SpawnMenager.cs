using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DNSCoreMechanics.ScriptObjects;
//using DNSCoreMechanics.AI;

namespace DNSCoreMechanics.ManagersCore
{

    //public class SpawnManager<T> : MonoBehaviour where T : EntityAI
    public class SpawnManager<T> : MonoBehaviour
    {
        //public static SpawnManager<T> Instance { get; private set; }

        [SerializeField] EntityBaseSO[] entities;
        [SerializeField] GameObject entity_prefab;
        GameObject parentContainer;

       /* private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }*/

        /// <summary>Method used to initialize required configs, this method should be called in start method.</summary>
        /// <param name="parentContainer">Add velocity to entity movement.</param>
        protected void initializeRequiredConfigs(string parentContainer)
        {
            this.parentContainer = GameObject.Find(parentContainer);
           
        }

        /// <summary>Method used to initialize required configs, this method should be called in start method.</summary>
        /// <param name="wave">Define the number of waves.</param>
        /// <param name="spwanPos">Vector3 object that defines the place where waves are be created.</param>
        public void NextWave(int wave, Vector3 spwanPos)
        {
            int entityCount = (int)Mathf.Pow(wave, 3f);
            //int enemyDifficulty = wave;

            for (int i = 0; i < entityCount; i++)
            {
               /* Vector3 spwanPos = new Vector3(
                    Random.Range(-10, 10),
                    Random.Range(6, 8)
                    );*/
                int entityIndex = Random.Range(0, entities.Length);
                GameObject entity = Instantiate(entity_prefab, spwanPos, new Quaternion(), parentContainer.transform);
                T entity_script = entity.GetComponent<T>();
               // entity_script.entityScriptObject = entities[entityIndex];
            }
        }

        /// <summary>Method used to generate a random position X and Y</summary>
        /// <param name="startPosX">Initial number used to generate range at X postion.</param>
        /// <param name="endPosX">Final number used to generate range at X postion.</param>
        /// <param name="startPosY">Initial number used to generate range at Y postion.</param>
        /// <param name="endPosY">Final number used to generate range at Y postion.</param>
        public static Vector3 GenerateRandomPosition2D(int startPosX, int endPosX, int startPosY, int endPosY)
        {
            return new Vector3(Random.Range(startPosX, endPosX),Random.Range(startPosY, endPosY));
        }
    }
}

