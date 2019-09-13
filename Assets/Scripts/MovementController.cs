using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public GameObject player;

    public Vector2 startPosition;
    public Vector2 direction;
    public bool directionChoosen;


    private void Start()
    {
       
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPosition = touch.position;

                    break;

                case TouchPhase.Moved:
                    direction = touch.position - startPosition;

                    break;

                case TouchPhase.Ended:
                    directionChoosen = true;
                    break;
            }
        }

        if (directionChoosen)
        {
            
            player.transform.position = direction;

            directionChoosen = false;
        }
    }
}
