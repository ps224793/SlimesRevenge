using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private GameObject owner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        owner = gameObject.transform.parent.gameObject;
        if (collision.tag== "Floor" || collision.tag=="Projectile")
        {
            Destroy(gameObject);
            return;
        }
        if (owner.tag != collision.tag && collision.tag != "Barrier" && collision.tag != "Platform" && collision.tag != "RangeCollider")
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.Health--;
            Destroy(gameObject);
            return;
        }
        Invoke("OnDestroy",10);
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
        return;
    }
}
