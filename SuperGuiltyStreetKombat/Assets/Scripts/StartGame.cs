using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void fighting()
    {
        SceneManager.LoadScene(1);
    }

    public void tutorial()
    {
        SceneManager.LoadScene(2);
    }

    public void back()
    {
        SceneManager.LoadScene(0);
    }

    public void playercontrols()
    {
        SceneManager.LoadScene(3);
    }
}
