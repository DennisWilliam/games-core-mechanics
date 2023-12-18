using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DNSCoreMechanics.AI;

namespace DNSCoreMechanics.ManagersCore
{

    public class GameManagerCore : MonoBehaviour
    {
        protected static GameManagerCore Instance { get; private set; }
        GameObject entityParent;
        [SerializeField] protected string entityParentTag;
        [SerializeField] protected int wave;
        [SerializeField] GameObject[] spawnMangers;

        private void Awake()
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
        }

        // Start is called before the first frame update
        void Start()
        {
            entityParent = GameObject.Find(entityParentTag);
        }

        // Update is called once per frame
        void Update()
        {
            if (AllEntitiesWereDetroyed())
            {
                for (int i=0; i< spawnMangers.Length; i++)
                {
                    //wave++;
                    Vector3 spwanPos = new Vector3(
                    Random.Range(-10, 10),
                    Random.Range(6, 8)
                    );
                   // SpawnManager<EnemyCoreAI> a = spawnMangers[i].GetComponent<SpawnManager<EnemyCoreAI>>();
                   // a.NextWave(2, spwanPos);
                }
               
            }
        }

        bool AllEntitiesWereDetroyed()
        {
            return entityParent.transform.childCount == 0;
        }
    }
}

