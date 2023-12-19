using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeaponBehavior
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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAI)
        {
            AIShooting();
        }
        else
        {
            Shooting();
        }
    }

    public void BulletsVelocity()
    {
        throw new System.NotImplementedException();
    }

    public void ChangeAmmo()
    {
        throw new System.NotImplementedException();
    }

    public void Reload()
    {
        throw new System.NotImplementedException();
    }

    public void Shooting()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

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
            ExecuteMouseClickAction();
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

    public void Special()
    {
        throw new System.NotImplementedException();
    }

    public void AIShooting()
    {
        Vector3 rotation = player.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canShoot = true;
                timer = 0;
            }
        }
        if (canShoot && isAI)
        {

            canShoot = false;
            ExecuteMouseClickAction();
        }
    }
}
