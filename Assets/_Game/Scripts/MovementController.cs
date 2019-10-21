using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Transform playerPos;
    public GameObject player;
    private Rigidbody2D rb;
    public LineRenderer lr;

    
    Vector3 lineStartPos;
    Vector3 lineEndPos;

    private float distance;

    public float forceMulti = 0.5f;
    public float setDrag = 1;
    private Vector3 startPos;
    private Vector2 direction;
    private bool directionChoosen = false;
    private Vector3 touchDragDistance;
    private CameraFollow cameraFollow;

    public float delay = 0.5f;



    private void Awake()
    {
        rb = player.GetComponent<Rigidbody2D>();
        playerPos = player.GetComponent<Transform>();
        
        
    }

    private void Update()
    {
        DestroyLineRenderer();
    }



    private void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        //timeCompelete = false;
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            if (Input.touchCount > 0)
            {


                Touch touch = Input.GetTouch(0);
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                startPos.z = 0f;
                startPos = playerPos.position;
                lr.positionCount = 2;

                switch (touch.phase)
                {
                    case TouchPhase.Began:

                        //rb.bodyType = RigidbodyType2D.Dynamic;
                        //cameraFollow.HandleZoom(true);
                        startPos = touchPos;
                        //StartCoroutine(SetRBComponent(delay, true));
                        rb.gravityScale = 0;
                        rb.mass = 0.1f;

                        rb.drag = setDrag;
                        lr.enabled = true;
                        lineStartPos = playerPos.position;
                        lr.SetPosition(0, lineStartPos);
                        lr.useWorldSpace = true;

                        break;

                    case TouchPhase.Moved:
                        lineEndPos = touchPos;
                        lr.SetPosition(0, lineStartPos);
                        lr.SetPosition(1, lineEndPos);

                        break;

                    case TouchPhase.Ended:
                        lr.enabled = false;
                        //cameraFollow.HandleZoom(false);
                        distance = touchPos.magnitude;
                        direction = touchPos - startPos;

                        rb.AddForce(direction * forceMulti);

                        rb.gravityScale = 1;
                        rb.mass = 1;
                        rb.drag = 0;
                        //StartCoroutine(SetRBComponent(delay, false));


                        Debug.Log("SLUT!");
                        directionChoosen = true;

                        break;
                }
            }

            lineStartPos = playerPos.position;
        }

        
    }

    private void DestroyLineRenderer()
    {
        if (player == null && lr != null)
        {
            Destroy(lr.gameObject);
        }
    }
}
