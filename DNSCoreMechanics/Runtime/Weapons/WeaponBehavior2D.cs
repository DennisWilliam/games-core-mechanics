using System.Collections;
using UnityEngine;


namespace DNSCoreMechanics.Weapons
{

    public class WeaponBehavior2D:MonoBehaviour
    {
        [Header("Shooting RigidBody Settings")]
        protected bool canShoot;
        Vector3 mousePos;
        Camera cam;
        Rigidbody2D rb;
        public float force;

        [Header("Shooting Settings")]
        [SerializeField] GameObject bullet;
        public Transform bulletTransform;
        public delegate void OnMouseClickDelegate();
        public OnMouseClickDelegate m_methodToCall;
        [SerializeField] float timer;
        [SerializeField] float timeBetweenFiring;

        [Header("AI Settings")]
        [SerializeField] bool isAI;
        GameObject player;

        protected void initializeRequiredWeaponBehaviorConfigs()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            canShoot = true;
        }

        private void Start()
        {
            initializeRequiredWeaponBehaviorConfigs();
        }

        private void Update()
        {
            if (!isAI)
            {
                OnShoot();
            }
            else
            {
                OnAIShoot();
            }
        }

        //Make object follow the mouse pointer direction
        public void WeaponAim(Camera cam, GameObject weapon_gameObject)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(
                mousePos.y - weapon_gameObject.transform.position.y,
                mousePos.x - weapon_gameObject.transform.position.x
                ) * Mathf.Rad2Deg;

            weapon_gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        
        public void OnUseGun(
            GameObject weapon_gameObject, 
            GameObject bullet_prefab, 
            GameObject bullet_parent, 
            Transform initialBulletPos_transform,
            int timeToDestroyBullet,
            bool isAI
            )
        {
            GameObject bullet;
            if (isAI)
            {

             bullet = Instantiate(
                bullet_prefab,
                initialBulletPos_transform.position,
                Quaternion.Euler(new Vector3(0, 0, initialBulletPos_transform.rotation.eulerAngles.z - 90))
                );
            }
            else
            {

             bullet = Instantiate(
                    bullet_prefab,
                    initialBulletPos_transform.position,
                    weapon_gameObject.transform.rotation,
                    bullet_parent.transform
                    );
            }

            bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(2, 0), ForceMode2D.Impulse);
            Destroy(bullet, timeToDestroyBullet);
        }

        protected void OnShoot()
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 rotation = mousePos - transform.position;
            float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotationZ);

            m_methodToCall = ExecuteMouseClickAction;
            DetectMouseClick(m_methodToCall);
        }

        protected void OnAIShoot()
        {
            Vector3 rotation = player.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, rotationZ);

            m_methodToCall = ExecuteMouseClickAction;
            DetectMouseClick(m_methodToCall);
        }

        public void DetectMouseClick(OnMouseClickDelegate methodToCall)
        {
            
            if (!canShoot)
            {
                timer += Time.deltaTime;
                if (timer > timeBetweenFiring)
                {
                    canShoot = true;
                    timer = 0;
                }
            }
           
            //Debug.Log(canShoot+" + "+!isAI);
            if (Input.GetMouseButton(0) && canShoot && !isAI)
            {
                canShoot = false;
                methodToCall();
            }else if(canShoot && isAI)
            {
                
                canShoot = false;
                methodToCall();
            }
        }

        private void ExecuteMouseClickAction()
        {
            GameObject bulletInstance;
            bulletInstance = Instantiate(
                    bullet,
                    bulletTransform.position,
                    Quaternion.identity
                    );

            
            //Instantiate(bullet, bulletTransform.position, Quaternion.identity);
            Destroy(bulletInstance, 5);
        }
    }


}

