using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject scoreTextScreen;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text scoreFinal;
    [SerializeField] private float initialScrollSpeed;

    private int score;
    private float timer;
    private float scrollSpeed;

    private void Start()
    {
        gameOverScreen.SetActive(false);
        scoreTextScreen.SetActive(false);
    }

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        { 
            Instance = this;
        }
    }

    void Update()
    {
        UpdateScore();
        UpdateSpeed();
        if (score == 0)
        {
            ShowScoreTextT();
        }
    }

    public void ShowGameOverScreen()
    {
        scoreFinal.text = "Puntuación: " + score;
        gameOverScreen.SetActive(true);
    }

    public void ShowScoreTextT()
    {
        scoreTextScreen.SetActive(true);
    }

    public void ShowScoreText()
    {
        scoreTextScreen.SetActive(false);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    private void UpdateScore()
    {
        int scorePerSeconds = 10;

        timer += Time.deltaTime;
        score = (int)((timer * scorePerSeconds) - 15);
        scoreText.text = string.Format("{0:00000}", score);
    }

    public float GetScrollSpeed()
    {
        return scrollSpeed;
    }

    public void UpdateSpeed()
    {
        float speedDivider = 10f;
        scrollSpeed = initialScrollSpeed + timer / speedDivider;
    }
}
