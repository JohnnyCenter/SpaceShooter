using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    public void Restart()
    {
        Debug.Log("Restarted");
        SceneManager.LoadScene("Prototype");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("QuitLogged");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Lukas_Prototype");
    }
}
