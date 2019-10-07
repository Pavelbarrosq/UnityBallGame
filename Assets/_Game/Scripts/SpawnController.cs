using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject objectiveBallPrefab;
    public int maxPool = 20;

    public float xOffset = 2f;
    public float yOffset = 2f;
    bool startGame = false;

    Vector3 center;
    Vector3 size;


    private Vector2 screenBounds;
    public List<GameObject> objPool = new List<GameObject>();

    private void Awake()
    {
        

        
    }

    private void Start()
    {
        AddToPool();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));


    }

    void AddToPool()
    {
        for (int i = 0; i < maxPool; i++)
        {
            Vector3 pos = center + new Vector3(Random.Range(-screenBounds.x / 2, screenBounds.x / 2),
                                               Random.Range(-screenBounds.y / 2, screenBounds.y / 2),
                                               0);

            GameObject obj = Instantiate(objectiveBallPrefab, pos, Quaternion.identity);

            objPool.Add(obj);
        }

        

    }

    void AddToWorldRandom()
    {
        foreach (GameObject obj in objPool)
        {
            obj.transform.position = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), Random.Range(-screenBounds.y, screenBounds.y));
        }
    }

    private Vector3 GetRandomInRectangle(float width, float height)
    {
        float x = Random.Range(0, width);
        float y = Random.Range(0, height);
        return new Vector3(x - xOffset, y - yOffset, 0);
    }
}
