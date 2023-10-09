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


    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Vector2 mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
            GameObject newprojectile = Instantiate(projectile, new Vector2(transform.position.x, transform.position.y), Quaternion.identity, transform);
            newprojectile.GetComponent<Rigidbody2D>().velocity = mousePos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
