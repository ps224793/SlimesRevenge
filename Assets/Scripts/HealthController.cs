using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class HealthController : MonoBehaviour
{

    [SerializeField]
    private int health;
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
                transform.position = new Vector2(0, 0);
                health = 3;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
