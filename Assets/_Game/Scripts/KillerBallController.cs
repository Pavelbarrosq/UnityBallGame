using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerBallController : MonoBehaviour
{
    GameObject playerRef;
    private GameObject lr;
    // Start is called before the first frame update
    void Start()
    {

        playerRef = GameObject.FindGameObjectWithTag("Player");
        lr = GameObject.FindGameObjectWithTag("TouchLine");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {

            Debug.Log("Player Killed");
            Destroy(playerRef);
            Destroy(lr);

            lr.SetActive(false);
        }
    }
}
