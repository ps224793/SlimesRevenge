using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class HealthController : MonoBehaviour
{

    [SerializeField]
    private int health;

    private SceneControler sceneControler = new SceneControler();
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            if (gameObject.tag == "Player")
            {
                sceneControler.ResetActiveScene();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
