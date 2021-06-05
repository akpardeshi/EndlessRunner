using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class C_GameManager : MonoBehaviour
{
    enum State { Start , Running , GameOver } ;
    State g_GameState ;

    [SerializeField] C_PlayerControls g_Player;
    [SerializeField] C_EnemyManager g_EnemyManager ;
    [SerializeField] GameObject g_StartPanel ;
    [SerializeField] GameObject g_ScorePanel ;
    [SerializeField] GameObject g_GameOverPanel ;
    [SerializeField] Text g_ScoreText ;
    [SerializeField] C_ScoreManager g_ScoreManager ;

    // Start is called before the first frame update
    void Start()
    {
        g_GameState = State.Start ;
    }

    // Update is called once per frame
    void Update()
    {
        m_GetInput() ;
        m_CheckGameOver () ;
    }

    void m_GetInput()
    {
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            if ( g_GameState == State.Start )
            {
                m_StartGame() ;
            }
        }

        if ( Input.GetKeyDown(KeyCode.R))
        {
            if ( g_GameState == State.GameOver )
            {

                m_RestartGame() ;
            }
        }
    }

    void m_StartGame()
    {
        g_Player.gameObject.SetActive(true);
        g_EnemyManager.gameObject.SetActive(true);
        g_ScorePanel.SetActive(true);
        g_StartPanel.SetActive(false);
        g_GameState = State.Running ;
    }

    void m_RestartGame()
    {
        SceneManager.LoadScene( SceneManager. GetActiveScene().buildIndex);
    }

    void m_CheckGameOver ()
    {
        if ( g_Player.g_BoolGameOver )
        {
            g_ScorePanel.SetActive(false);
            g_ScoreText.text = "SCORE : " + g_ScoreManager.g_IntPlayerScore.ToString() ;
            g_EnemyManager.gameObject.SetActive(false);
            g_ScoreManager.gameObject.SetActive(false);
            g_GameOverPanel.SetActive( true );
            g_GameState = State.GameOver ;
        }
    }
}
