using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeShot : MonoBehaviour
{

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float verticalProjectileSpeed;
    [SerializeField]
    private float horizontalProjectileSpeed;
    [SerializeField]
    private Camera playerCam;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float shootCooldown;
    private bool canShoot = true;
    [SerializeField]
    private float maxShootvelocity;
    [SerializeField]
    private float minShootvelocity;


    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(1)&&canShoot)
        {
            canShoot = false;
            Vector2 mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootVector = mousePos - rb.position;
            shootVector = new Vector2(horizontalProjectileSpeed* shootVector.x, verticalProjectileSpeed+ shootVector.y);
            shootVector += rb.velocity;
            if (shootVector.magnitude >= maxShootvelocity)
            {
                float scaler = 2 / shootVector.magnitude;
                shootVector = shootVector * scaler;
            }
            else if (shootVector.magnitude <= minShootvelocity)
            {
                float scaler = 2 * shootVector.magnitude;
                shootVector = shootVector * scaler;
            }

            GameObject newprojectile = Instantiate(projectile,transform);
            newprojectile.GetComponent<Rigidbody2D>().velocity = shootVector;
            Invoke("ShootBool", shootCooldown);
        }
    }

    void ShootBool()
    {
        canShoot =true;
    }
}
