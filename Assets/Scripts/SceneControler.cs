using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            ResetActiveScene();
        }
    }

    public void ResetActiveScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
