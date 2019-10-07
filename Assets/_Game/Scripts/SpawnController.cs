using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject objectiveBallPrefab;
    public int maxPool = 20;

    public float parralaxFactor;
    public float fieldWidth = 20f;
    public float fieldHeight = 25f;
    float xOffset;
    float yOffset;


    public GameObject[] objPool;
    Transform cameraTransform;

    private Vector2 screenBounds;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;

        xOffset = fieldWidth * 0.5f;
        yOffset = fieldHeight * 0.5f;
    }

    private void Start()
    {
        //screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        SpawnObjectiveBalls();
        
    }



    private void Update()
    {
       
    }


    private void SpawnObjectiveBalls()
    {
        for (int i = 0; i < maxPool; i++)
        {
            GameObject objBall = Instantiate(objectiveBallPrefab) as GameObject;
            //Vector2 randomPos = new Vector2(Random.Range(-screenBounds.x, screenBounds.x) * 2 + parralaxFactor, Random.Range(-screenBounds.y, screenBounds.y) * 2 + parralaxFactor);
            Vector3 randomPos = GetRandomInRectangle(fieldWidth, fieldHeight) + transform.position;


            objBall.transform.position = randomPos;
        }
    }

    private Vector3 GetRandomInRectangle(float width, float height)
    {
        float x = Random.Range(0, width);
        float y = Random.Range(0, height);
        return new Vector3(x - xOffset, y - yOffset, 0);
    }
}
