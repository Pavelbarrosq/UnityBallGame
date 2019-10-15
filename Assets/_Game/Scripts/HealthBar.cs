using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 1.0f)] private float fillAmount;
    private float maxHealth = 1f;
    [SerializeField] private float subtractHealth = 0.1f;
    [SerializeField] private Image content;
    [SerializeField] private int second = 1;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        HandleHealthBar();
        StartCoroutine(SubtraktHealthPerSecond());
    }

    private void Update()
    {
        if (player != null)
        {
            if (content.fillAmount == 0f)
            {
                Destroy(player);
            }
        }


    }

    private void HandleHealthBar()
    {
        content.fillAmount = maxHealth;
    }

    IEnumerator SubtraktHealthPerSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(second);
            content.fillAmount -= subtractHealth;
        }
    }
}
