using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 1.0f)] private float fillAmount;
    private float maxHealth = 1f;
    [SerializeField] private float subtractHealth = 0.05f;
    [SerializeField] private Image content;
    [SerializeField] private int second = 1;
    [SerializeField] private float lerpSpeed = 2f;
    private GameObject player;
    private Object playerExplosion;

    private void Awake()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        playerExplosion = Resources.Load("PlayerExplosion");
    }

    private void Start()
    {
        StartHealth();
        StartCoroutine(SubtraktHealthPerSecond());
    }

    private void Update()
    {

        if (player != null)
        {
            if (content.fillAmount == 0f || content.fillAmount < 0)
            {
                StopCoroutine(SubtraktHealthPerSecond());
                content.fillAmount = 0f;
                PlayerDeathExplosion();
                Destroy(player);
                
            }
        }


    }

    private void StartHealth()
    {
        content.fillAmount = maxHealth;
    }

    IEnumerator SubtraktHealthPerSecond()
    {
        if (player != null)
        {
            while (true)
            {
                yield return new WaitForSeconds(second);
                content.fillAmount -= subtractHealth;

            }
        }
        
    }

    private void PlayerDeathExplosion()
    {
        if (player != null)
        {
            GameObject explosion = (GameObject)Instantiate(playerExplosion);
            explosion.transform.position = player.transform.position;
        }

    }
}
