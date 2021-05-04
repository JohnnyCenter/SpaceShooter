using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public GameObject loadingSreen;
    public Slider slider;
    public void Restart(int sceneIndex)
    {
        Debug.Log("Restarted");
        SceneManager.LoadScene("Prototype");
        StartCoroutine(LoadAsyncchronously(sceneIndex));
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("QuitLogged");
    }

    public void MainMenu(int sceneIndex)
    {
       // SceneManager.LoadScene("Lukas_Prototype");
        StartCoroutine(LoadAsyncchronously(sceneIndex));
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
