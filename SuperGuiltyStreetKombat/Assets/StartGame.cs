using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] Sprite menu;
    [SerializeField] Sprite tutorial;

    Image m_image;

    private void Start()
    {
        m_image = GetComponent<Image>();
        if (!m_image)
        {
            Debug.LogError("Please add image component!");
            return;
        }
    }

    public void GoToTutorial()
    {
        m_image.sprite = tutorial;
    }

    public void GoToMenu()
    {
        m_image.sprite = menu;
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }

}
