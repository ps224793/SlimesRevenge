using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject platforms;

    [SerializeField]
    private float duration;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical")==-1)
        {
            platforms.GetComponent<Collider2D>().enabled = false;
            Invoke("Enable",duration);
        }
    }
    private void Enable()
    {
        if (Input.GetAxisRaw("Vertical") != -1)
        {
            platforms.GetComponent<Collider2D>().enabled = true;
            return;
        }
        Invoke("Enable", duration);
    }
}
