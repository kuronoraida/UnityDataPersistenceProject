using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public GameObject GameOverButton;
    public Text HighScoreText;

    private bool m_Started = false;
    private int m_Points;
    public int highScore;
    public string highScoreName;

    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        highScore = AppManager.Instance.highScore;
        highScoreName = AppManager.Instance.highScoreName;
        HighScoreText.text = highScoreName + "'s high score: " + highScore;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        ScoreText.text = AppManager.Instance.playerName + "'s score : " + m_Points;
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = AppManager.Instance.playerName + "'s score : " + m_Points;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        GameOverButton.SetActive(true);
        if (m_Points > highScore)
        {
            highScore = m_Points;
            highScoreName = AppManager.Instance.playerName;
            AppManager.Instance.highScoreName = AppManager.Instance.playerName;
            AppManager.Instance.highScore = highScore;
            UpdateHighScoreText();
        }
        AppManager.Instance.SaveData();
    }

    public void UpdateHighScoreText()
    {
        HighScoreText.text = highScoreName + "'s high score: " + highScore;
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
