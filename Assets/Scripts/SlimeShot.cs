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


    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Vector2 mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 jumpVector = mousePos - rb.position;
            jumpVector = new Vector2(horizontalProjectileSpeed*jumpVector.x, verticalProjectileSpeed+jumpVector.y);
            GameObject newprojectile = Instantiate(projectile,transform);
            newprojectile.GetComponent<Rigidbody2D>().velocity = jumpVector;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
