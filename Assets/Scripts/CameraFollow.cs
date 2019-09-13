using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraDistance = 40.0f;
    public float yPos = 2.0f;


    private void Awake()
    {
        GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
    }

    private void FixedUpdate()
    {
        CameraPosition(yPos);
    }

    private void CameraPosition(float position)
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + position, transform.position.z);
    }

    
}
