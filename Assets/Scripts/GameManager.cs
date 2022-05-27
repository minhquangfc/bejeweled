using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;

    private int score, highScore;
    public Text scoreText, highScoreText;

    private float time;
    public Text timeText;

    public bool started, gameOver;
    public static bool gameIsPause = false;
    public GameObject gameOverPanel,gameUIPanel,pausePanel;
    public Text gameOverScore, gameOverHighScore;
    public Button playAgain;
    void Start()
    {
        instance = GetComponent<GameManager>();

        score = 0;
        scoreText.text = "Score: " + score;
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score: " + highScore;

        time = 45;
        gameOver = false;
        UpdateTime();
    }

    // Update is called once per frame

    void Update()
    {
        if (started)
        {
            time -= Time.deltaTime;
            UpdateTime();
            if (time <= 0)
            {
                GameOver();
            }
        }
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score: " + score;
        time += 5;
        UpdateTime();
    }
    public void StartTimer()
    {
        time = 45;
        
        started = true;
    }
    public void UpdateTime()
    {
        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = Mathf.Floor(time % 60).ToString("00");
        timeText.text = string.Format("Time: {0}:{1}", minutes, seconds);
    }
    private void GameOver()
    {
        time = 0;
        UpdateTime();
        started = false;
        gameOver = true;
        gameOverPanel.SetActive(true);
        gameUIPanel.SetActive(false);
        gameOverScore.text = "Final Score: " + score;
        if (score > highScore)
        {
            gameOverHighScore.text = "High Score: " + score;
            PlayerPrefs.SetInt("HighScore", score);
        }
        else
        {
            gameOverHighScore.text = "High Score: " + score;
        }
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void PauseButton()
    {
        
        pausePanel.SetActive(true);
        started = false;
        gameIsPause = true;
        gameOver = true;
    }
    public void Resume()
    {
        
        pausePanel.SetActive(false);
        started = true;
        gameIsPause = false;
        gameOver = false;
    }    
}
