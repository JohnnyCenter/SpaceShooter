using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TutorialMenu : MonoBehaviour
{
    [SerializeField]
    GameObject howToPlayButton, videoButton, exitButton, tutorialTitle, nextButton, backButton;
    RectTransform htpbr, vbr, ebr, tt;
    Animator anim;
    private int page;

    private void Awake()
    {
        htpbr = howToPlayButton.GetComponent<RectTransform>();
        vbr = videoButton.GetComponent<RectTransform>();
        ebr = exitButton.GetComponent<RectTransform>();
        tt = tutorialTitle.GetComponent<RectTransform>();

        howToPlayButton.SetActive(false);
        videoButton.SetActive(false);
        exitButton.SetActive(false);
        tutorialTitle.SetActive(false);

        anim = GetComponent<Animator>();

        page = -1;
    }

    public void FromMainToTutorial()
    {
        StartCoroutine("TutorialEnterTransition");
    }

    private IEnumerator TutorialEnterTransition()
    {
        howToPlayButton.SetActive(true);
        videoButton.SetActive(true);
        exitButton.SetActive(true);
        tutorialTitle.SetActive(true);
        yield return null;
        tt.DOAnchorPos(new Vector2(0, 900), 0);
        htpbr.DOAnchorPos(new Vector2(0, -900), 0);
        htpbr.DOScale(new Vector2(1, 1), 0);
        vbr.DOAnchorPos(new Vector2(0, -900), 0);
        vbr.DOScale(new Vector2(1, 1), 0);
        ebr.DOAnchorPos(new Vector2(0, -900), 0);
        ebr.DOScale(new Vector2(0.5f, 1), 0);
        yield return null;
        tt.DOJumpAnchorPos(new Vector2(0, 400), 20, 1, 0.5f);
        htpbr.DOJumpAnchorPos(new Vector2(0, 75), 20, 1, 0.5f);
        vbr.DOJumpAnchorPos(new Vector2(0, -75), 20, 1, 0.5f);
        ebr.DOJumpAnchorPos(new Vector2(0, -500), 20, 1, 0.5f);
    }

    public void TutorialExitTransition()
    {
        tt.DOAnchorPosX(800, 0.5f);
        htpbr.DOScale(new Vector2(1, 0.5f), 0.25f);
        htpbr.DOAnchorPosX(-700, 0.5f);
        vbr.DOScale(new Vector2(1, 0.5f), 0.25f);
        vbr.DOAnchorPosX(700, 0.5f);
        ebr.DOScale(new Vector2(0.5f, 0.5f), 0.25f);
        ebr.DOAnchorPosX(-700, 0.5f);
    }

    public void HowToPlayEnter()
    {
        anim.SetTrigger("HowToPlay");
        page = 1;
        anim.ResetTrigger("HowToPlayExit");
        backButton.SetActive(false);
    }

    public void NextPage()
    {
        switch (page)
        {
            case 1:
                anim.SetInteger("Page", 2);
                page += 1;
                backButton.SetActive(true);
                break;
            case 2:
                anim.SetInteger("Page", 3);
                page += 1;
                break;
            case 3:
                anim.SetInteger("Page", 4);
                page += 1;
                break;
            case 4:
                anim.SetInteger("Page", 5);
                page += 1;
                break;
            case 5:
                anim.SetInteger("Page", 6);
                page += 1;
                nextButton.SetActive(false);
                break;
        }
    }

    public void BackPage()
    {
        switch (page)
        {
            case 2:
                anim.SetInteger("Page", 1);
                page -= 1;
                backButton.SetActive(false);
                break;
            case 3:
                anim.SetInteger("Page", 2);
                page -= 1;
                break;
            case 4:
                anim.SetInteger("Page", 3);
                page -= 1;
                break;
            case 5:
                anim.SetInteger("Page", 4);
                page -= 1;
                break;
            case 6:
                anim.SetInteger("Page", 5);
                page -= 1;
                nextButton.SetActive(true);
                break;
        }
    }

    public void HowToPlayExit()
    {
        anim.SetTrigger("HowToPlayExit");
        anim.ResetTrigger("HowToPlay");
        backButton.SetActive(false);
        anim.SetInteger("Page", 1);
    }
}
