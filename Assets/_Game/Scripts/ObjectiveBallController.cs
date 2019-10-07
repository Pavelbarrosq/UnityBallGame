using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveBallController : MonoBehaviour
{
    private Material matWhite;
    private Material matDefault;
    public float pushUp = 0.5f;
    public float waitToKillTime = 0.3f;

    private SpriteRenderer sr;
    private CircleCollider2D cc;

    Rigidbody2D playerRefRB;

    //public Rigidbody2D playerRB;
    private UnityEngine.Object explosionRef;


    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerRefRB = player.GetComponent<Rigidbody2D>();


        cc = playerRefRB.GetComponentInParent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        explosionRef = Resources.Load("Explosion");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            playerRefRB.bodyType = RigidbodyType2D.Static;
            //playerRB.gravityScale = 0;
            //playerRB.mass = 0.1f;
            //playerRB.drag = 10000;
            sr.material = matWhite;
            Debug.Log("Target hit!");


            StartCoroutine(KillSelf(waitToKillTime));

            
            //PushPlayerUp();
        }
    }

    private void PushPlayerUp()
    {
        //playerRB.drag = 0;


        playerRefRB.AddForce(Vector2.up * pushUp);



        //playerRB.mass = 1;

        //playerRB.gravityScale = 1;
    }

    private IEnumerator KillSelf(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
            AddParticleExplosion();
            playerRefRB.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void AddParticleExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);

        explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
