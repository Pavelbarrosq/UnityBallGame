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


    private void Awake()
    {
        
    }

    //private void Update()
    //{
    //    GetComponent<UnityEngine.Camera>().orthographicSize = ((Screen.height / 2) / cameraDistance);
    //}

    private void Update()
    {
        float newZoom = standardZoom + (Mathf.Abs(Vector2.Distance(transform.position, playerTransform.position))) / 4;
        Vector3 playerToCam = basePosition - playerTransform.position;
        Vector3 desiredPosition = playerTransform.position + offset;
        desiredPosition.z = -10f;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        //offset.y = yOffset + newZoom / 6;

        currentCamera.orthographicSize = Mathf.SmoothDamp(currentCamera.orthographicSize, newZoom, ref zoomVel, zoomSpeed);

        //Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        //transform.position = smoothPosition;
        

    }
    private void Start()
    {
        basePosition = transform.position;
        //yOffset = offset.y;
    }

    private void CameraPosition()
    {



        //transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + position, transform.position.z);
    }
}
