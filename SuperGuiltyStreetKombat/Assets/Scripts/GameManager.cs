using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    private bool isGameOver = false;
    [SerializeField] GameObject gameOverDisplay;
    [SerializeField] TMP_Text gameOverText;
    string winner;

    public static Player player
    {
        get
        {
            if(gameManager.m_player == null)
            {
                gameManager.m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }

            return gameManager.m_player;
        }
    }

    private Player m_player;

    public static Player2 player2
    {
        get
        {
            if (gameManager.m_player2 == null)
            {
                gameManager.m_player2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player2>();
            }

            return gameManager.m_player2;
        }
    }

    private Player2 m_player2;

    public static MoveManager moveManager
    {
        get
        {
            if (gameManager.m_moveManager == null)
            {
                gameManager.m_moveManager = GameObject.FindGameObjectWithTag("MoveManager").GetComponent<MoveManager>();
            }

            return gameManager.m_moveManager;
        }
    }

    private MoveManager m_moveManager;

    private void Awake()
    {
        gameManager = this;
    }

    public bool GameOver()
    {
        return isGameOver;
    }

    public void GoToScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
