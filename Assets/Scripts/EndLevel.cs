using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{    
    SceneControler sceneControler = new SceneControler();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Als dit object collide met de speler roep ik de sceneController aan, die reset de scene
        if (collision.gameObject.tag == "Player") {
        sceneControler.ResetActiveScene();
        }
    }
}
