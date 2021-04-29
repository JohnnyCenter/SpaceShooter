using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource mainmenu;
    [SerializeField] bool isOn = false;

    private void Start()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("GameStart");


    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QuitLogged");
    }

    private void Update()
    {
        if(isOn == false)
        {
            mainmenu.Play();
            isOn = true;
        }
    }
}
