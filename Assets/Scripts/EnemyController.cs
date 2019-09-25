using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Material matWhite;
    private Material matDefault;
    public float waitToKillTime = 0.3f;

    public Rigidbody2D playerRB;
    public float pushUp = 0.5f;

    private UnityEngine.Object explosionRef;
    

    SpriteRenderer sr;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();


        matWhite = Resources.Load("WhiteFlash",typeof(Material)) as Material;
        matDefault = sr.material;
        explosionRef = Resources.Load("Explosion");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            playerRB.bodyType = RigidbodyType2D.Static;
            //playerRB.gravityScale = 0;
            //playerRB.mass = 0.1f;
            //playerRB.drag = 10000;
            sr.material = matWhite;
            Debug.Log("Target hit!");
            

            StartCoroutine(KillEnemy(waitToKillTime));

            playerRB.bodyType = RigidbodyType2D.Dynamic;
            PushPlayerUp();
        }
    }

    private void PushPlayerUp()
    {
        //playerRB.drag = 0;
        

        playerRB.AddForce(Vector2.up * pushUp);

        

        //playerRB.mass = 1;

        //playerRB.gravityScale = 1;
    }

    private IEnumerator KillEnemy(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
            AddParticleExplosion();
        }
    }

    private void AddParticleExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(explosionRef);

        explosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
    }
}
