using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowBallController : MonoBehaviour
{
    GameObject playerRef;
    Rigidbody2D playerRB;
    private Transform target;
    public float distance = 10f;
    public float speed;
    private Object playerDamageParticle;
    private Object followBallExplosion;
    private Image content;
    [SerializeField, Range(0f, 0.3f)] private float subtractHealth; 
    
    private void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        playerRB = playerRef.GetComponent<Rigidbody2D>();
        target = playerRef.GetComponent<Transform>();
        playerDamageParticle = Resources.Load("PlayerDamageParticle");
        followBallExplosion = Resources.Load("FollowBallExplosion");
        content = GameObject.FindGameObjectWithTag("Content").GetComponent<Image>();

    }


    private void Update()
    {
        if (gameObject != null && playerRef != null)
        {
            if (Vector2.Distance(target.position, transform.position) <= distance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Destroy obj
            Destroy(gameObject);

            //add particles
            FollowBallExplosion();
            PlayerParticle();

            // -Health
            HealthDamage(subtractHealth);


        }
    }

    private void FollowBallExplosion()
    {
        // Creating a clone of the particle and set it to obj position
        GameObject explosion = Instantiate(followBallExplosion) as GameObject;
        explosion.transform.position = transform.position;

    }

    private void PlayerParticle()
    {
        // Creating a clone of the particle and set it to obj position
        GameObject particle = Instantiate(playerDamageParticle) as GameObject;
        particle.transform.position = target.transform.position;
    }

    private void HealthDamage(float healthToSubtract)
    {
        
        content.fillAmount -= healthToSubtract;
    }
   
}
