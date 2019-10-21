using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveBallController : MonoBehaviour
{
    private Material matWhite;
    private Material matDefault;
    public float pushUp = 0.5f;
    public float waitToKillTime = 0.15f;
    public float dynamicTimeFloat = 0.15f;
    private Image content;
    [SerializeField, Range(0.0f, 0.3f)] float addToHealth;
    private ScoreCounter scoreCounter;


 
    private CircleCollider2D cc;

    Rigidbody2D playerRefRB;

    Vector3 screenBounds;

    //public Rigidbody2D playerRB;
    private UnityEngine.Object explosionRef;

    private void Awake()
    {
        scoreCounter = FindObjectOfType<ScoreCounter>();
    }

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerRefRB = player.GetComponent<Rigidbody2D>();

        content = GameObject.FindGameObjectWithTag("Content").GetComponent<Image>();
        cc = playerRefRB.GetComponentInParent<CircleCollider2D>();
        
        explosionRef = Resources.Load("Explosion");
    }


    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            scoreCounter.AddScore();
            playerRefRB.bodyType = RigidbodyType2D.Static;
           
            Debug.Log("Target hit!");

            //Gain little health
            AddPlayerHealth(addToHealth);

            

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
            AddParticleExplosion();

            //Add Audio
            FindObjectOfType<AudioManager>().Play("Explosion");

            //Change players bodyType
            playerRefRB.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    

    private void AddParticleExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);

        explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }

    private void AddPlayerHealth(float healthToAdd)
    {
        content.fillAmount += healthToAdd;
    }

    
}
