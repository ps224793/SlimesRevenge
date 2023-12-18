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

    void Update()
    {
        if (health == 0)
        {
            //Als de speler dood gaat
            if (gameObject.tag == "Player")
            {
                sceneControler.ResetActiveScene();
            }
            // Als het niet de speler is die dood gaat
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
