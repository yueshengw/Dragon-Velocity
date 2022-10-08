using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Tutorial (Castle)");
    }

    public void Credits()
    {
        SceneManager.LoadScene("End Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
