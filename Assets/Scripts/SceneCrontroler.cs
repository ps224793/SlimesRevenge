using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCrontroler : MonoBehaviour
{



    // Update is called once per frame
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
