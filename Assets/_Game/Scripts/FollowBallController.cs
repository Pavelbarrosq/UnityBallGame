using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBallController : MonoBehaviour
{
    GameObject playerRef;
    Rigidbody2D playerRB;
    private Transform target;
    public float distance = 10f;
    public float speed;
    
    private void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        playerRB = playerRef.GetComponent<Rigidbody2D>();
        target = playerRef.GetComponent<Transform>();

    }


    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit with small damage");
        }
    }
}
