using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingSreen;
    public Slider slider;
    private float progress;
    public Image sliderfill;
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsyncchronously(sceneIndex));
        

    }
    IEnumerator LoadAsyncchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingSreen.SetActive(true);

       

        while (progress < 1f)
        {
            progress = Mathf.Clamp01(operation.progress / 9F);
            Debug.Log(progress);
            sliderfill.fillAmount = progress;

            slider.value = progress;

            yield return null;
        }
    }
}
