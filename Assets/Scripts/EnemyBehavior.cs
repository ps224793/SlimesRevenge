using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private float directionTimer = 0;
    [SerializeField]
    public float directionTimerGoal = 5;
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

    [SerializeField]
    private float knockbackMultiplier;
    void Start()
    {
        playerRB = player.GetComponent<Rigidbody2D>();
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Counts up directionTimer
        //directionTimer += Time.deltaTime;

        //// Randomizes enemy direction when TimerGoal is reached
        //if (directionTimer >= directionTimerGoal)
        //{
        //    direction = Random.Range(-1, 1);
        //    if (direction == 0)
        //    {
        //        direction = 1;
        //    }
        //    directionTimer = 0;
        //}

    }
    private void FixedUpdate()
    {
        Shoot();

        //moves enemy
        rb2d.velocity = new Vector2(speed * direction, rb2d.velocity.y);
        if (rb2d.velocity.x < -maxVelocity)
        {
            rb2d.velocity = new Vector2(-maxVelocity, rb2d.velocity.y);
        }
        if (rb2d.velocity.x > maxVelocity)
        {
            rb2d.velocity = new Vector2(maxVelocity, rb2d.velocity.y);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If enemy collides with another object; change direction value

        if (collision.tag == "Player" || collision.tag == "Barrier")
        {
            collisionTrigger();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If enemy collides with another object; change direction value

        if ( collision.gameObject.tag == "Barrier")
        {
            collisionTrigger();
        }
        else if ( collision.gameObject.tag == "Player")
        {
            Vector2 enemyPos = gameObject.transform.position;
            Vector2 playerPos = player.transform.position;
            Vector2 knockbackVector = playerPos - enemyPos;
            knockbackVector = knockbackVector * knockbackMultiplier;
            Debug.Log(knockbackVector.magnitude);
            playerRB.AddForce(knockbackMultiplier * knockbackVector, ForceMode2D.Impulse);
        }
    }

    private void collisionTrigger()
    {
        if (direction == -1)
        {
            direction = 1;
            return;
        }
        direction = -1;
    }

    private void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            Vector2 enemyPos = gameObject.transform.position;
            Vector2 playerPos = player.transform.position;
            Vector2 shootVector = playerPos-enemyPos;
            shootVector = new Vector2(horizontalProjectileSpeed * shootVector.x, verticalProjectileSpeed * shootVector.y);
            //if (shootVector.magnitude >= maxShootvelocity)
            //{
            //    float scaler = 2 / shootVector.magnitude;
            //    shootVector = shootVector * scaler;
            //}
            //else if (shootVector.magnitude <= minShootvelocity)
            //{
            //    float scaler = 2 * shootVector.magnitude;
            //    shootVector = shootVector * scaler;
            //}

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
