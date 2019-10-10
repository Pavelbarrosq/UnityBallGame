using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public GameObject objPrefab;
    public List<GameObject> objPool;
    public int maxObj = 20;
    GameObject obj;

    private Vector3 cameraBounds;

    private void Awake()
    {
        objPool = new List<GameObject>();
        
    }

    private void Update()
    {
        cameraBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    private void Start()
    {

        
        AddPrefabsToList();
        
    }

    private void AddPrefabsToList()
    {
        for (int i = 0; i < maxObj; i++)
        {

            GameObject objectiveBall = Instantiate(objPrefab) as GameObject;
            objectiveBall.transform.position = transform.position;
            objectiveBall.SetActive(false);
            objPool.Add(objectiveBall);

            

        }

        Debug.Log("All objects " + objPool.Count);
    }

}
