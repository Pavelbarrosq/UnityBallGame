using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomPushBallController : MonoBehaviour
{
    private Rigidbody2D playerRB;

    private Vector2 randomDegree;

    public int addForce = 100;

    private float secondsToWait = 0.05f;

    private Object explosionRef;

    private Image content;
    private ScoreCounter scoreCounter;
    public float halfLife = 0.5f;



    private void Awake()
    {
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        randomDegree = Random.insideUnitCircle;

        explosionRef = Resources.Load("SlingShotExplosion");

        content = GameObject.FindGameObjectWithTag("Content").GetComponent<Image>();
        scoreCounter = FindObjectOfType<ScoreCounter>();

        
    }

    private void Update()
    {
        randomDegree = Random.insideUnitCircle;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            scoreCounter.AddScore();
            scoreCounter.AddScore();

            AddHealth(halfLife);

            playerRB.bodyType = RigidbodyType2D.Static;

            Debug.Log("Random degree is" + randomDegree);

            StartCoroutine(PushPLayer());

        }
    }

    IEnumerator PushPLayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsToWait);
            Destroy(this.gameObject);
            Explosion();

            FindObjectOfType<AudioManager>().Play("SlingShot");

            playerRB.bodyType = RigidbodyType2D.Dynamic;
            playerRB.AddForce(randomDegree * addForce);
        }
    }

    private void Explosion()
    {
        GameObject explosion = Instantiate(explosionRef) as GameObject;
        explosion.transform.position = this.transform.position;
    }

    private void AddHealth(float health)
    {
        content.fillAmount += health;
    }
}
