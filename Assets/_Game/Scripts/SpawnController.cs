using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour
{

    public GameObject objectBallPrefab;
    public int objectivsPerField = 15;

    public float fieldSize = 15f;

    private Vector2Int lastField;


    List<GameObject> objectiveBalls = new List<GameObject>();


    public List<Color> colors;
    int colorIndex = 0;

    private void Awake()
    {


        Vector2Int currentField = GetCurrentField();

        PopulateField(currentField);

        PopulateNeighbouringFields(currentField);

        PopulateOuterNeighbouringFields(currentField);

    }


    private void Update()
    {

        Vector2Int currentField = GetCurrentField();

        if (Time.frameCount % 2 == 0)
        {

            if (lastField != currentField ) // om vi har bytt fällt
            {

                for(int i = objectiveBalls.Count - 1; i >= 0; i--)
                {
                   
                    if (objectiveBalls[i] == null)
                    {
                        objectiveBalls.RemoveAt(i);
                    }
                    else if (GetField(objectiveBalls[i].transform.position) != currentField
                        && !NeighbouringFields(currentField).Contains<Vector2Int>(GetField(objectiveBalls[i].transform.position))) // om boll int är i current eller neighbour
                    {
                        Destroy(objectiveBalls[i]);
                        objectiveBalls.RemoveAt(i); 
                    }
                }

                PopulateOuterNeighbouringFields(currentField);
                lastField = currentField;
            }
        }

        //for (int i = 0; i < objectiveBalls.Count; i++)
        //{
        //    if (objectiveBalls[i] == null)
        //    {
        //        objectiveBalls.RemoveAt(i);
        //    }
        //}


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

    private void PopulateOuterNeighbouringFields(Vector2Int centerField)
    {

        foreach (Vector2Int field in OuterNeighbouringFields(centerField))
        {
            PopulateField(field);
        }

    }


    private void PopulateField(Vector2Int field)
    {
        Color color = colors[colorIndex];
        if (colorIndex < colors.Count - 2)
            colorIndex++;
        else
            colorIndex = 0;

        for (int i = 0; i < objectivsPerField; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-fieldSize, fieldSize) + (field.x * 2 * fieldSize), Random.Range(-fieldSize, fieldSize) + (field.y * 2 * fieldSize), 0);

            GameObject obj =  Instantiate(objectBallPrefab, pos, Quaternion.identity);
            objectiveBalls.Add(obj);

            obj.GetComponent<SpriteRenderer>().color = color;


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


    private Vector2Int[] OuterNeighbouringFields(Vector2Int field)
    {

        Vector2Int[] neighbours = new Vector2Int[16];


        neighbours[0] = new Vector2Int(field.x - 2, field.y + 2);
        neighbours[1] = new Vector2Int(field.x - 1, field.y + 2);
        neighbours[2] = new Vector2Int(field.x, field.y + 2);
        neighbours[3] = new Vector2Int(field.x + 1, field.y + 2);
        neighbours[4] = new Vector2Int(field.x + 2, field.y + 2);

        neighbours[5] = new Vector2Int(field.x - 2, field.y + 1);
        neighbours[6] = new Vector2Int(field.x + 2, field.y + 1);

        neighbours[7] = new Vector2Int(field.x - 2, field.y);
        neighbours[8] = new Vector2Int(field.x + 2, field.y);

        neighbours[9] = new Vector2Int(field.x - 2, field.y - 1);
        neighbours[10] = new Vector2Int(field.x + 2, field.y - 1);

        neighbours[11] = new Vector2Int(field.x - 2, field.y - 2);
        neighbours[12] = new Vector2Int(field.x - 1, field.y - 2);
        neighbours[13] = new Vector2Int(field.x, field.y - 2);
        neighbours[14] = new Vector2Int(field.x + 1, field.y - 2);
        neighbours[15] = new Vector2Int(field.x + 2, field.y - 2);
        
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
