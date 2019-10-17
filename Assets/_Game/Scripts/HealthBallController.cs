using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBallController : MonoBehaviour
{
    private Object healthExplosion;
    private Image content;
    private float maxHealth = 1f;
    private Rigidbody2D rb;
    public float waitToKillTime = 0.15f;



    private void Awake()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        healthExplosion = Resources.Load("HealthExplosion");
        content = GameObject.FindGameObjectWithTag("Content").GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            rb.bodyType = RigidbodyType2D.Static;
            // gain full HP
            SetFullHealth(maxHealth);

            StartCoroutine(KillSelf(waitToKillTime));
        }


    }

    private IEnumerator KillSelf(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            //Destroy Gameobject
            Destroy(this.gameObject);

            // Add Particle Explosion
            HealthExplosion();

            //Change players bodyType
            rb.bodyType = RigidbodyType2D.Dynamic;
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
