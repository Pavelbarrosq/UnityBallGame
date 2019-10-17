using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverObject;

    private void Start()
    {
        gameOverObject.SetActive(false);
    }

    public void EndGame()
    {
        Debug.Log("GameOver");
        gameOverObject.SetActive(true);
    }

    public void RestartGame()
    {
        Debug.Log("Button clicked");
        SceneManager.LoadScene("Game");
    }
}
