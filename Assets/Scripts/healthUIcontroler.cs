using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthUIcontroler : MonoBehaviour
{
    [SerializeField]
    private HealthController health;
    [SerializeField]
    private SpriteRenderer hpImage;

    [SerializeField]
    private Sprite hp3;
    [SerializeField]
    private Sprite hp2;
    [SerializeField]
    private Sprite hp1;
    

    // Start is called before the first frame update
    void Start()
    {
        hpImage.sprite = hp3;
    }

    // Update is called once per frame
    void Update()
    {
        switch (health.Health)
        {
            case 3:
                hpImage.sprite = hp3;
                break;
            case 2:
                hpImage.sprite = hp2;
                break;
            case 1:
                hpImage.sprite = hp1;
                break;

        }

    }
}
