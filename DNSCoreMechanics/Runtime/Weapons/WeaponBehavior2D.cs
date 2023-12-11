using System.Collections;
using UnityEngine;


namespace DNSCoreMechanics.Weapons
{

    public class WeaponBehavior2D:MonoBehaviour
    {
        protected bool canShoot = true;

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
    }
}

