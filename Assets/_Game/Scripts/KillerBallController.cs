using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillerBallController : MonoBehaviour
{
    GameObject playerRef;
    private PlayerController playerController;
    private GameObject lr;
    private Rigidbody2D rb;
    private Object playerExplosionRef;
    public float waitToKillTime = 0.3f;
    private Image content;
    // Start is called before the first frame update
    private void Awake()
    {
        content = GameObject.FindGameObjectWithTag("Content").GetComponent<Image>();
    }

    void Start()
    {

        
        playerRef = GameObject.FindGameObjectWithTag("Player");
        rb = playerRef.GetComponent<Rigidbody2D>();
        lr = GameObject.FindGameObjectWithTag("TouchLine");
        playerExplosionRef = Resources.Load("PlayerExplosion");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {

            rb.bodyType = RigidbodyType2D.Static;

            Debug.Log("Player Killed");

            StartCoroutine(BeforeDeath());

            SetHealthToZero();

            lr.SetActive(false);
        }
    }

    IEnumerator BeforeDeath()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitToKillTime);
            Destroy(playerRef);
            PlayerDeathExplosion();

        }
    }

    private void PlayerDeathExplosion()
    {
        if (playerRef != null)
        {
            GameObject explosion = (GameObject)Instantiate(playerExplosionRef);
            explosion.transform.position = playerRef.transform.position;
        }
        
    }

    private void SetHealthToZero()
    {
        content.fillAmount = 0;
    }


}
