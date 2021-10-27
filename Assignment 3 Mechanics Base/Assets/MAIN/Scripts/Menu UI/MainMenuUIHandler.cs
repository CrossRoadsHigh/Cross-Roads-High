using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : MonoBehaviour
{
    public GameObject HowToPlay;
    public GameObject Credits;
    public GameObject LevelsButtons;

    void Start()
    {
        HowToPlay.SetActive(false);
        Credits.SetActive(false);
        LevelsButtons.SetActive(false);
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("Level 1 - New Models");
    }

    public void OnHowToPlayButton()
    {
        if (HowToPlay.activeInHierarchy)
        {
            HowToPlay.SetActive(false);
        }
        else
        {
            Credits.SetActive(false);
            LevelsButtons.SetActive(false);
            HowToPlay.SetActive(true);
        }
    }

    public void OnLevelsButton()
    {
        if (LevelsButtons.activeInHierarchy)
        {
            LevelsButtons.SetActive(false);
        }
        else
        {
            Credits.SetActive(false);
            HowToPlay.SetActive(false);
            LevelsButtons.SetActive(true);
        }
    }

    public void OnCreditsButton()
    {
        if (Credits.activeInHierarchy)
        {
            Credits.SetActive(false);
        }
        else
        {
            HowToPlay.SetActive(false);
            LevelsButtons.SetActive(false);
            Credits.SetActive(true);
        }
    }

    public void OnQuitButton()
    {
        Debug.Log("Game has Quit");
        Application.Quit();
    }

    public void OnPolygonButton()
    {
        System.Diagnostics.Process.Start("https://assetstore.unity.com/packages/3d/environments/urban/polygon-city-low-poly-3d-art-by-synty-95214");
    }

    public void OnSnapsButton()
    {
        System.Diagnostics.Process.Start("https://assetstore.unity.com/packages/3d/environments/urban/snaps-prototype-school-154693");
    }

    public void OnOutlineButton()
    {
        System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=Rn_yJ516dVQ");
    }

    public void OnLevelButton(int number)
    {
        switch (number)
        {
            case 1:
                SceneManager.LoadScene("Level 1 - New Models");
                break;
            case 2:
                SceneManager.LoadScene("Level 2 - New Models");
                break;
            case 3:
                SceneManager.LoadScene("Level 3 - New Models");
                break;
        }
    }
}
