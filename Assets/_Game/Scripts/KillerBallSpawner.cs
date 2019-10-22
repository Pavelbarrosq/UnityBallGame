using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KillerBallSpawner : MonoBehaviour
{
    public GameObject killerBallPrefab;


    public int killersPerField = 3;

    [SerializeField] private float fieldSize = 15f;

    private Vector2Int lastField;


    List<GameObject> killerBalls = new List<GameObject>();


    private void Awake()
    {
        SpawnOnAwake(killerBallPrefab, killersPerField, killerBalls);


    }

    private void SpawnOnAwake(GameObject ballPrefab, int ballsPerField, List<GameObject> ballList)
    {
        Vector2Int currentField = GetCurrentField();

        PopulateField(currentField, ballPrefab, ballsPerField, ballList);

        PopulateNeighbouringFields(currentField, ballPrefab, ballsPerField, ballList);

        PopulateOuterNeighbouringFields(currentField, ballPrefab, ballsPerField, ballList);
    }


    private void Update()
    {
        PopulateOnUpdate(killerBallPrefab, killersPerField, killerBalls);

    }

    private void PopulateOnUpdate(GameObject ballPrefab, int ballsPerField, List<GameObject> ballList)
    {
        Vector2Int currentField = GetCurrentField();

        if (Time.frameCount % 2 == 0)
        {

            if (lastField != currentField) // om vi har bytt fällt
            {

                for (int i = ballList.Count - 1; i >= 0; i--)
                {

                    if (ballList[i] == null)
                    {
                        ballList.RemoveAt(i);
                    }
                    else if (GetField(ballList[i].transform.position) != currentField
                        && !NeighbouringFields(currentField).Contains<Vector2Int>(GetField(ballList[i].transform.position))) // om boll int är i current eller neighbour
                    {
                        Destroy(ballList[i]);
                        ballList.RemoveAt(i);
                    }
                }

                PopulateOuterNeighbouringFields(currentField, ballPrefab, ballsPerField, ballList);
                lastField = currentField;
            }
        }

    }


    private void PopulateNeighbouringFields(Vector2Int centerField, GameObject ballPrefab, int ballsPerField, List<GameObject> ballList)
    {
        // Vector2Int currentField = GetCurrentField();

        foreach (Vector2Int field in NeighbouringFields(centerField))
        {
            PopulateField(field, ballPrefab, ballsPerField, ballList);
        }

        lastField = centerField;

    }

    private void PopulateOuterNeighbouringFields(Vector2Int centerField, GameObject ballPrefab, int ballsPerField, List<GameObject> ballList)
    {

        foreach (Vector2Int field in OuterNeighbouringFields(centerField))
        {
            PopulateField(field, ballPrefab, ballsPerField, ballList);
        }

    }


    private void PopulateField(Vector2Int field, GameObject ballPrefab, int ballsPerField, List<GameObject> ballList)
    {


        for (int i = 0; i < ballsPerField; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-fieldSize, fieldSize) + (field.x * 2 * fieldSize), Random.Range(-fieldSize, fieldSize) + (field.y * 2 * fieldSize), 0);

            GameObject obj = Instantiate(ballPrefab, pos, Quaternion.identity);
            ballList.Add(obj);


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
