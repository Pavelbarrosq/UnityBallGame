using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Transform playerPos;
    public GameObject player;
    private Rigidbody2D rb;
    private float distance;

    public float forceMulti = 0.5f;
    public float setDrag = 1;
    public Vector3 startPos;
    public Vector2 direction;
    public bool directionChoosen = false;
    private Vector3 touchDragDistance;
    private CameraFollow cameraFollow;

    public float delay = 0.5f;
    private bool orNah = false;


    private void Awake()
    {
        rb = player.GetComponent<Rigidbody2D>();
        playerPos = player.GetComponent<Transform>();
        
    }

    private void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        //timeCompelete = false;
    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            startPos.z = 0f;
            startPos = playerPos.position;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //cameraFollow.HandleZoom(true);
                    startPos = touchPos;
                    //StartCoroutine(SetRBComponent(delay, true));
                    rb.gravityScale = 0;
                    rb.mass = 0.1f;
                    rb.drag = setDrag;
                    break;

                case TouchPhase.Moved:


                    break;

                case TouchPhase.Ended:
                    //cameraFollow.HandleZoom(false);
                    distance = touchPos.magnitude;
                    direction = touchPos - startPos;

                    rb.AddForce(direction * forceMulti);
                    rb.gravityScale = 1;
                    rb.mass = 1;
                    rb.drag = 0;
                    //StartCoroutine(SetRBComponent(delay, false));
                    

                    Debug.Log(distance);
                    directionChoosen = true;

                    break;
            }
        }
    }

    //private IEnumerator SetRBComponent(float delay, bool orNah)
    //{
    //    if (!orNah)
    //    {
    //        yield return new WaitForSeconds(delay);
    //        rb.gravityScale = 1;
    //        rb.mass = 1;
    //        rb.drag = 0;
    //    } else
    //    {
    //        yield return new WaitForSeconds(delay);
    //        rb.gravityScale = 0;
    //        rb.mass = 0.1f;
    //        rb.drag = 1;
    //    }
    //}

}
