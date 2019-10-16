using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBallController : MonoBehaviour
{
    private Object healthExplosion;
    private Image content;
    private float maxHealth = 1f;



    private void Awake()
    {
        healthExplosion = Resources.Load("HealthExplosion");
        content = GameObject.FindGameObjectWithTag("Content").GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            // gain full HP
            SetFullHealth(maxHealth);

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

    private void SetFullHealth(float fullHealth)
    {
        content.fillAmount = fullHealth;
    }

}
