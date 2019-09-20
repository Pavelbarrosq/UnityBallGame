using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraDistance = 40f;
    public float yPos = 0.0f;


    private void Awake()
    {
        
    }

    private void Update()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
    }

    private void FixedUpdate()
    {
        CameraPosition(yPos);
        

    }
    private void Start()
    {

    }

    private void CameraPosition(float position)
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + position, transform.position.z);
    }
}
