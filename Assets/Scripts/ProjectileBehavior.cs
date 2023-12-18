using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private GameObject owner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Zet de eigenaar van het object gelijk aan de ouder
        owner = gameObject.transform.parent.gameObject;

        // Als dit object collide met de vloer of een ander object wordt deze vernietigd
        if (collision.tag== "Floor" || collision.tag=="Projectile")
        {
            Destroy(gameObject);
            return;
        }
        // Als de ouder van dit object niet het zelfde is als het geraakte object maar het is wel de speler of een vijand
        else if (owner.tag != collision.tag && collision.tag == "Player" || owner.tag != collision.tag && collision.tag == "Enemy")
        {
            // Haal dan de HealthController van het geraakte object op en haal een leven van het geraakte object af
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            healthController.Health--;
            //Vernietig daarna dit object (het projectiel)
            Destroy(gameObject);
            return;
        }
        // Als er na 10 seconde niks geraakt is wordt het projectiel automatisch verwijderd
        Invoke("OnDestroy",10);
    }
    private void OnDestroy()
    {
        Destroy(gameObject);
        return;
    }
}
