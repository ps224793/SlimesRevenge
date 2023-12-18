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



    // Update is called once per frame
    void FixedUpdate()
    {
        //Als de speler op muisknop 1 drukt en canShoot true is
        if (Input.GetMouseButton(1)&&canShoot)
        {
            canShoot = false;
            //Haalt de positie van de muis in de wereld op
            Vector2 mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
            //Haalt de playey positie van e muis positie af om de afstand tussen de twee punten te berekenen
            Vector2 shootVector = mousePos - rb.position;
            //Vermenigvuldig deze vector met de horizontale en verticale vertical projectile speeds
            shootVector = new Vector2(horizontalProjectileSpeed* shootVector.x, verticalProjectileSpeed* shootVector.y);
            // Voeg dan nog de huidige velocity van dit object toe aan deze vector
            shootVector += rb.velocity;

            // Maak een nieuw projectiel aan en zet de transform gelijk aan de transform van dit object
            GameObject newprojectile = Instantiate(projectile,transform);
            // Zet de velocity van dit nieuw projectiel gelijk aan de shootVector
            newprojectile.GetComponent<Rigidbody2D>().velocity = shootVector;
            // Zet een timer aan die na shootCooldown aantal seconde de ShootBool Methode aanroept
            Invoke("ShootBool", shootCooldown);
        }
    }

    void ShootBool()
    {
        canShoot =true;
    }
}
