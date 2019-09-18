using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Transform playerPos;
    public GameObject player;
    private Rigidbody2D rb;
    private Vector2 distance;

    public float forceMulti = 200;
    public Vector3 startPos;
    public Vector2 direction;
    public bool directionChoosen;
    private Vector3 touchDragDistance;

    private void Awake()
    {
        rb = player.GetComponent<Rigidbody2D>();
        playerPos = player.GetComponent<Transform>();
    }

    private void Start()
    {
         
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            // Vector3 camPos = Camera.main.ScreenToWorldPoint(playerPos.position);
            //Transform stopPos;

            startPos.z = 0f;

            startPos = playerPos.position;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    
                    //startPosition = playerToCamPos;
                    startPos = touchPos;//playerPos.position;
                    rb.velocity = Vector3.zero;
                    rb.gravityScale = 0;
                    rb.mass = 0.1f;
                    break;

                case TouchPhase.Moved:

                    //distance = touchPos - startPos;
                    Debug.Log("Moving...");
                    
                    break;

                case TouchPhase.Ended:
                    //stopPos = playerPos;
                    rb.mass = 1f;
                    rb.gravityScale = 1;
                    //direction = touchPos - startPos;
                    distance = touchPos - startPos;
                    // direction = touchPos;
                    rb.AddForce(distance * forceMulti);
                    //touchDragDistance = startPosition + direction;

                    Debug.Log("Distance: " + distance);
                    Debug.Log("Start!!!!!" + startPos);

                    //directionChoosen = true;

                    break;
            }
        }

        //if (directionChoosen)
        //{

            

        //    directionChoosen = false;
        //}
    }

    

    private void FixedUpdate()
    {
        
    }
}
