using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public GameObject objectBallPrefab;
    public int objectivsPerField = 15;

    private float fieldSize = 30f;

    private Vector2Int lastField;


    List<GameObject> objectiveBalls = new List<GameObject>();


    private void Awake()
    {


        Vector2Int currentField = GetCurrentField();

        PopulateField(currentField);

        PopulateNeighbouringFields(currentField);

    }


    private void Update()
    {

        Vector2Int currentField = GetCurrentField();


        if (Time.frameCount % 1 == 0)
        {

            if (lastField != currentField)
            {

                for(int i = objectiveBalls.Count - 1; i >= 0; i--)
                {
                    if (GetField(objectiveBalls[i].transform.position) != currentField)
                    {
                        Destroy(objectiveBalls[i]);
                        objectiveBalls.RemoveAt(i); 
                    }
                }

                PopulateNeighbouringFields(currentField);
                lastField = currentField;
            }
        }

        for (int i = 0; i < objectiveBalls.Count; i++ )
        {
            if (objectiveBalls[i] == null)
            {
                objectiveBalls.RemoveAt(i);
            }
        }


    }


    private void PopulateNeighbouringFields(Vector2Int centerField)
    {
        // Vector2Int currentField = GetCurrentField();

        foreach (Vector2Int field in NeighbouringFields(centerField))
        {
            PopulateField(field);
        }

        lastField = centerField;

    }



    private void PopulateField(Vector2Int field)
    {
        for (int i = 0; i < objectivsPerField; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-fieldSize, fieldSize) + (field.x * 2 * fieldSize), Random.Range(-fieldSize, fieldSize) + (field.y * 2*fieldSize), 0);

            GameObject obj =  Instantiate(objectBallPrefab, pos, Quaternion.identity);
            objectiveBalls.Add(obj);
        }
    }

    private Vector2Int[] NeighbouringFields(Vector2Int field)
    {
        Vector2Int[] neighbours = new Vector2Int[8];

        neighbours[0] = new Vector2Int(field.x - 1, field.y + 1);
        neighbours[1] = new Vector2Int(field.x, field.y + 1);
        neighbours[2] = new Vector2Int(field.x + 1, field.y + 1);
        neighbours[3] = new Vector2Int(field.x - 1, field.y);
        neighbours[4] = new Vector2Int(field.x + 1, field.y);
        neighbours[5] = new Vector2Int(field.x - 1, field.y - 1);
        neighbours[6] = new Vector2Int(field.x, field.y - 1);
        neighbours[7] = new Vector2Int(field.x + 1, field.y - 1);

        return neighbours;
    }


    private Vector2Int GetCurrentField()
    {
        return GetField(transform.position);
    }


    private Vector2Int GetField(Vector2 pos)
    {

        return new Vector2Int(Mathf.RoundToInt(pos.x / (fieldSize * 2)), Mathf.RoundToInt(pos.y / (fieldSize * 2)));

    }
}
