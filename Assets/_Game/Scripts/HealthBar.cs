using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 1.0f)] private float fillAmount;
    private float timeStartedLerping;
    private float maxHealth = 1f;
    [SerializeField] private float subtractHealth = 0.5f;
    [SerializeField] private Image content;
    [SerializeField] private float second = 1;
    [SerializeField] private float lerpSpeed = 2f;
    private GameObject player;
    private Object playerExplosion;
    private GameManager gameManager;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
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
            if (content.fillAmount <= 0f )
            {               
                StopCoroutine(SubtraktHealthPerSecond());
                content.fillAmount = 0f;
                PlayerDeathExplosion();
                Destroy(player);
                gameManager.EndGame();
                FindObjectOfType<AudioManager>().Play("Explosion");
                FindObjectOfType<AudioManager>().Stop("Music");                
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

    public float Lerp(float start, float end, float timeStartedLerping, float lerpTime)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;

        float procentageCompleted = timeSinceStarted / lerpTime;

        var result = Mathf.Lerp(start, end, procentageCompleted);

        return result;
    }
}
