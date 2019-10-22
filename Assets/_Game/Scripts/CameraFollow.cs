using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera currentCamera;
    Vector3 basePosition;
    public Transform playerTransform;
    public Vector3 offset;
    private float yOffset;
    //public float yPos = 0.0f;
    public float smoothSpeed = 0.25f;
    public float standardZoom = 11f;
    public Vector3 velocity = Vector3.zero;
    public float zoomVel;
    public float zoomSpeed = 1f;


   
    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            float newZoom = standardZoom + (Mathf.Abs(Vector2.Distance(transform.position, playerTransform.position))) / 4;
            Vector3 playerToCam = basePosition - playerTransform.position;
            Vector3 desiredPosition = playerTransform.position + offset;
            desiredPosition.z = -10f;

            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);


            currentCamera.orthographicSize = Mathf.SmoothDamp(currentCamera.orthographicSize, newZoom, ref zoomVel, zoomSpeed);
        }
        

    }
    private void Start()
    {
        basePosition = transform.position;

    }

}
