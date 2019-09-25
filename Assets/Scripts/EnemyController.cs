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
    

    SpriteRenderer sr;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();


        matWhite = Resources.Load("WhiteFlash",typeof(Material)) as Material;
        matDefault = sr.material;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            playerRB.drag = 10000;
            sr.material = matWhite;
            Debug.Log("Target hit!");
            

            StartCoroutine(KillEnemy(waitToKillTime));

            PushPlayerUp();
        }
    }

    private void PushPlayerUp()
    {
        playerRB.drag = 0;

        playerRB.AddForce(Vector2.up * pushUp);
        
    }

    private IEnumerator KillEnemy(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
        }
    }
}
