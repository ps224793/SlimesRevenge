using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaControler : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private SceneControler SceneControler = new SceneControler();
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject == player)
        {
            SceneControler.ResetActiveScene();
        }
        
    }
}
