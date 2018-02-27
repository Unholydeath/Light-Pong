using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : Singleton<Game>
{
    [SerializeField] GameObject m_titlePage;
    [SerializeField] GameObject m_pause;
    [SerializeField] GameObject m_gameOver;
    [SerializeField] GameObject m_gameHUD;
    [SerializeField] GameObject m_victory;
    [SerializeField] TextMeshProUGUI m_victoryBallCount;
    [SerializeField] TextMeshProUGUI m_ballCountHUD;
    public TextMeshProUGUI playerScoreHUD;

    [SerializeField] Ball m_ball;
    [SerializeField] GameObject m_ballContainer;
    [SerializeField] GameObject m_spawnPoint;
    [SerializeField] GameObject m_brickContainer;

    Ball m_newBall;

    [SerializeField] [Range(.5f, 5.0f)] float m_ballMoveTimer = 3.0f;
    float m_ballCountDown;
    int m_ballCount = 3;
    public int playerScore = 0;
    bool m_ballIsImmobile = false;

    private void Start()
    {
        Time.timeScale = 0.0f;
        m_ballCountDown = m_ballMoveTimer;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !m_pause.activeInHierarchy)
        {
            m_pause.SetActive(true);
            Time.timeScale = 0;
        }

        if (m_ballContainer.transform.childCount == 0 && m_ballCount > 0)
        {
            m_ballCount--;
            m_newBall = Instantiate(m_ball, m_spawnPoint.transform.position, Quaternion.identity, m_ballContainer.transform);
            m_ballIsImmobile = true;
            m_ballCountHUD.text = "Balls Left: " + m_ballCount;
        }

        if (m_ballIsImmobile)
        {
            m_ballCountDown -= Time.deltaTime;

            if(m_ballCountDown <= 0.0f)
            {
                m_newBall.MoveBall();
                m_ballCountDown = m_ballMoveTimer;
                m_ballIsImmobile = false;
            }
        }

        if (Input.GetKey(KeyCode.F))
        {
            Time.timeScale = 2.0f;
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            Time.timeScale = 1.0f;
        }

        if(m_brickContainer.transform.childCount == 0)
        {
            WinGame();
        }

        if(m_ballCount == 0 && m_ballContainer.transform.childCount == 0)
        {
            Time.timeScale = 0.0f;
            m_gameOver.SetActive(true);
        }
    }


    public void StartGame()
    {
        m_titlePage.SetActive(false);
        m_gameHUD.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void Unpause()
    {
        m_pause.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartGame();
    }

    public void WinGame()
    {
        Time.timeScale = 0.0f;
        m_victory.SetActive(true);
        m_victoryBallCount.text = "Extra Balls: " + m_ballCount;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
