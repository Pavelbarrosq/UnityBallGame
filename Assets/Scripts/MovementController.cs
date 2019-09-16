using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Transform playerPos;

    public Vector2 startPosition;
    public Vector2 direction;
    public bool directionChoosen;
    private Vector2 touchDragDistance;


    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 playerToCamPos = Camera.main.ScreenToWorldPoint(playerPos.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPosition = playerToCamPos;

                    break;

                case TouchPhase.Moved:
                    direction = touchPos - startPosition;

                    break;

                case TouchPhase.Ended:

                    
                    touchDragDistance = startPosition + direction;
                    directionChoosen = true;
                    Debug.Log("Distance: " + touchDragDistance);
                    break;
            }
        }

        if (directionChoosen)
        {
            
            

            directionChoosen = false;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
