using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu Settings")]

    [Tooltip("When game is not paused this will be false therefor the deafult will always be false, if game is paused it should set to true")]
    public static bool GameIsPaused = false;

    [Tooltip("Add the options menu")]
    public GameObject OptionsMenu;
    public GameObject loadingSreen;
    public Slider slider;
    // Start is called before the first frame update


    public void RestartLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsyncchronously(sceneIndex));
       // Scene scene = SceneManager.GetActiveScene();
      //  SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f;
        OptionsMenu.SetActive(false);
        GameIsPaused = false;
    }
    public void Resume()
    {
        OptionsMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;


    }

    public void Pause()
    {
        OptionsMenu.SetActive(true);
        Time.timeScale = 0F;
        GameIsPaused = true;
    }
    public void LoadMenu(int sceneIndex)
    {
        StartCoroutine(LoadAsyncchronously(sceneIndex));
        Time.timeScale = 1f;
        Debug.Log("LOADMENU");
        SceneManager.LoadScene("Lukas_Prototype");

    }

    public void Remainpaused()
    {
        Time.timeScale = 0f;
        Debug.Log("Timescale 0");

    }

    

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    IEnumerator LoadAsyncchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingSreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 9F);
            Debug.Log(progress);

            slider.value = progress;

            yield return null;
        }
    }
}

