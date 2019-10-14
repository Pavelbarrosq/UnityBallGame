using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBallController : MonoBehaviour
{

    private Object healthExplosion;


    
    private void Awake()
    {
        healthExplosion = Resources.Load("HealthExplosion");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            // gain full HP

            //Destroy obj
            Destroy(gameObject);

            //add particle
            HealthExplosion();
        }
        

    }

    private void HealthExplosion()
    {
        if (gameObject != null)
        {
            GameObject particle = Instantiate(healthExplosion) as GameObject;
            particle.transform.position = transform.position;

        }
    }
}
