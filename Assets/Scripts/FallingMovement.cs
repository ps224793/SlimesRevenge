using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject platforms;

    [SerializeField]
    private float duration;

    void Update()
    {
        // Bij verticale input van -1 (naar beneden) 
        // Zet dan de collider van alle platformen uit
        if (Input.GetAxisRaw("Vertical")==-1)
        {
            platforms.GetComponent<Collider2D>().enabled = false;
            // Na duration aantal seconden wordt de Enable method aangeroepen
            Invoke("Enable",duration);
        }
    }
    private void Enable()
    {
        // Geen negatieve verticale input meer heeft wordt de collider van de platformen weer aangezet
        if (Input.GetAxisRaw("Vertical") != -1)
        {
            platforms.GetComponent<Collider2D>().enabled = true;
            return;
        }
        // Als de speler nog wel negatieve verticale input geeft dan wordt deze methode na duration aantal seconde weer uitgevoerd
        Invoke("Enable", duration);
    }
}
