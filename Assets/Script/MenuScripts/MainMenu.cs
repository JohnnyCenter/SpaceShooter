using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public AudioSource mainmenu;
    [SerializeField] bool isOn = false;

    [SerializeField]
    GameObject playButton, optionsButton, tutorialButton, extrasButton, exitButton, mainTitle;
    RectTransform pbr, obr, tbr, ebr, exbr, mt;
    TextMeshProUGUI playText, optionsText, tutorialText, extrasText;

    [SerializeField]
    GameObject OptionsMenu;
    Animator oma;

    private void Awake()
    {
        pbr = playButton.GetComponent<RectTransform>();
        obr = optionsButton.GetComponent<RectTransform>();
        tbr = tutorialButton.GetComponent<RectTransform>();
        ebr = extrasButton.GetComponent<RectTransform>();
        exbr = exitButton.GetComponent<RectTransform>();
        playText = playButton.GetComponentInChildren<TextMeshProUGUI>();
        optionsText = optionsButton.GetComponentInChildren<TextMeshProUGUI>();
        tutorialText = tutorialButton.GetComponentInChildren<TextMeshProUGUI>();
        extrasText = extrasButton.GetComponentInChildren<TextMeshProUGUI>();
        mt = mainTitle.GetComponent<RectTransform>();
        oma = OptionsMenu.GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine("MainEnterTransition");
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            MainExitTransition();
        }
    }

    public void MainExitTransition()
    {
        playText.enabled = false;
        tutorialText.enabled = false;
        optionsText.enabled = false;
        extrasText.enabled = false;
        pbr.DOScale(new Vector2(1.2f, 1), 0.25f);
        pbr.DOAnchorPosX(-700, 0.5f);
        obr.DOScale(new Vector2(1.2f, 1), 0.25f);
        obr.DOAnchorPosX(700, 0.5f);
        tbr.DOScale(new Vector2(1.2f, 1), 0.25f);
        tbr.DOAnchorPosX(-700, 0.5f);
        ebr.DOScale(new Vector2(1.2f, 1), 0.25f);
        ebr.DOAnchorPosX(700, 0.5f);
        exbr.DOAnchorPosX(500, 0.5f);
        mt.DOAnchorPosX(650, 0.5f);
    }

    IEnumerator MainEnterTransition()
    {
        yield return new WaitForSeconds(0.5f);
        playText.enabled = true;
        tutorialText.enabled = true;
        optionsText.enabled = true;
        extrasText.enabled = true;
        pbr.DOAnchorPos(new Vector2(-35, -900), 0);
        pbr.DOScale(new Vector2(1.2f, 1.5f), 0);
        obr.DOAnchorPos(new Vector2(-35, -900), 0);
        obr.DOScale(new Vector2(1.2f, 1.5f), 0);
        tbr.DOAnchorPos(new Vector2(-35, -900), 0);
        tbr.DOScale(new Vector2(1.2f, 1.5f), 0);
        ebr.DOAnchorPos(new Vector2(-35, -900), 0);
        ebr.DOScale(new Vector2(1.2f, 1.5f), 0);
        mt.DOAnchorPos(new Vector2(-180, 900), 0);
        //yield return new WaitForSeconds(0.5f);
        pbr.DOJumpAnchorPos(new Vector2(-35, 100), 20, 1, 0.5f);
        obr.DOJumpAnchorPos(new Vector2(-35, -50), 20, 1, 0.5f);
        tbr.DOJumpAnchorPos(new Vector2(-35, -200), 20, 1, 0.5f);
        ebr.DOJumpAnchorPos(new Vector2(-35, -350), 20, 1, 0.5f);
        exbr.DOAnchorPosX(160, 0.5f);
        mt.DOAnchorPosY(360, 0.5f);
        oma.ResetTrigger("ExitTransition");
        oma.ResetTrigger("EnterTransition");
        OptionsMenu.SetActive(false);
    }

    public void OptionsExitTransition()
    {
        oma.SetTrigger("ExitTransition");
        StartCoroutine("MainEnterTransition");
    }

    public void OptionsEnterTransition()
    {
        OptionsMenu.SetActive(true);
        oma.SetTrigger("EnterTransition");
    }

    public void MainMenuEnter()
    {
        StartCoroutine("MainEnterTransition");
    }
}
