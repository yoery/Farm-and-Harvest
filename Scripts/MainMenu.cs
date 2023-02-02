using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Playgame()
    {
        SceneManager.LoadScene(1);
    }

    public void Playgame2()
    {
        SceneManager.LoadScene(2);
    }

    public void Main()
    {
        SceneManager.LoadScene(0);
    }

    public void Congrats()
    {
        SceneManager.LoadScene(5);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}