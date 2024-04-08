using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

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
        if (gameManager != null)
        {
            Destroy(this);
        }

        gameManager = this;
        DontDestroyOnLoad(gameManager);
    }
}
