using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField]
    public float maxVelocity = 1;
    [SerializeField]
    public float speed = 1;

    public int direction = 1;

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float verticalProjectileSpeed;
    [SerializeField]
    private float horizontalProjectileSpeed;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float shootCooldown;
    private bool canShoot=true;

    [SerializeField]
    private float maxShootvelocity;
    [SerializeField]
    private float minShootvelocity;

    private Rigidbody2D playerRB;
    public bool inRange;

    [SerializeField]
    private float knockbackMultiplier;
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Shoot();

        //Beweegt de vijand met een snelheid van speed*direction
        rb2d.velocity = new Vector2(speed * direction, rb2d.velocity.y);
        //Als de velocity lager is dan de negatieve maxVelocity zet deze dan gelijk aan de negatieve maxVelocity
        if (rb2d.velocity.x < -maxVelocity)
        {
            rb2d.velocity = new Vector2(-maxVelocity, rb2d.velocity.y);
        }
        //Als de velocity hoger is dan de maxVelocity zet deze dan gelijk aan de maxVelocity

        if (rb2d.velocity.x > maxVelocity)
        {
            rb2d.velocity = new Vector2(maxVelocity, rb2d.velocity.y);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Als dit object een barriere raakt wordt de collision trigger method uitgevoerd

        if (collision.tag == "Barrier")
        {
            CollisionTrigger();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Als dit object een barriere raakt wordt de collision trigger method uitgevoerd

        if ( collision.gameObject.tag == "Barrier")
        {
            CollisionTrigger();
        }
        // Als de speler dit object raakt wordt de vector2 tussen de twee objecten berekend
        // Deze wordt vermenigvuldigd met een knockbackMultiplier
        // Deze vector wordt vervolgens als kracht toegevoegd aan de speler
        // Dit wordt gedaaan met Forcemode2d Impluse om een ommidelijke kracht te genereren
        else if ( collision.gameObject.tag == "Player")
        {
            Vector2 enemyPos = gameObject.transform.position;
            Vector2 playerPos = player.transform.position;
            Vector2 knockbackVector = playerPos - enemyPos;
            knockbackVector = knockbackVector * knockbackMultiplier;
            playerRB.AddForce(knockbackVector, ForceMode2D.Impulse);
        }
    }

    private void CollisionTrigger()
    {
        //Deze methode zet de direction int gelijk aan 1 of -1
        direction *= -1;
    }

    private void Shoot()
    {
        // Als canShoot en inRange true zijn
        // Zet canShoot op false
        // Bereken dan de vector2 tussen de speler en dit object dit heet nu de shootVector
        // Vemenigvuldig deze volgens met de horizontal en vertical projection speeds
        // Voeg dan nog de huidige velocity van dit object toe aan deze vector
        // Maak een nieuw projectiel aan en zet de transform gelijk aan de transform van dit object
        // Zet de velocity van dit nieuw projectiel gelijk aan de shootVector
        // Zet een timer aan die na shootCooldown aantal seconde de ShootBool Methode aanroept
        if (canShoot && inRange)
        {
            canShoot = false;
            Vector2 enemyPos = gameObject.transform.position;
            Vector2 playerPos = player.transform.position;
            Vector2 shootVector = playerPos-enemyPos;
            shootVector = new Vector2(horizontalProjectileSpeed * shootVector.x, verticalProjectileSpeed * shootVector.y);
            shootVector += rb2d.velocity;

            GameObject newprojectile = Instantiate(projectile, transform);
            newprojectile.GetComponent<Rigidbody2D>().velocity = shootVector;
            Invoke("ShootBool", shootCooldown);
        }
    }

    void ShootBool()
    {
        canShoot = true;
    }

}
