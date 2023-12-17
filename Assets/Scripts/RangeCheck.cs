using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Als de Speler zich binnen deze trigger bevind, zet dan de inRange Bool van de ouder van deze trigger op true
        if (collision.tag == "Player")
        {
            gameObject.GetComponentInParent<EnemyBehavior>().inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Als de Speler deze trigger verlaat, zet dan de inRange Bool van de ouder van deze trigger op false
        if (collision.tag == "Player")
        {
            // Try catch om een null reference te voorkomen bij het herladen van de scene
            try
            {
                gameObject.GetComponentInParent<EnemyBehavior>().inRange = false;
            }
            catch
            {

            }
        }
    }
}