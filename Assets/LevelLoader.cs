using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingSreen;
    public Slider slider;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsyncchronously(sceneIndex));
        

    }
    IEnumerator LoadAsyncchronously (int sceneIndex)
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
