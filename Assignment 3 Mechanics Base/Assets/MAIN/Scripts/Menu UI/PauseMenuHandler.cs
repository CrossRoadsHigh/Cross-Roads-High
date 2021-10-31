using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject PauseMenu;

    public GameObject Restart;
    public GameObject Resume;

    public GameObject DeadText;
    public GameObject WinText;
    public GameObject PausedText;

    public bool Level3 = false;
    public GameObject boss;


    private bool isPaused = false;
    private bool doOnce = true;


    void Start()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        Debug.Log(!boss.activeInHierarchy);
        if (Level3 && !boss.activeInHierarchy && doOnce)
        {
            Time.timeScale = 0;

            PauseMenu.SetActive(true);
            Restart.SetActive(false);
            DeadText.SetActive(false);
            Resume.SetActive(false);
            PausedText.SetActive(false);

            WinText.SetActive(true);

            doOnce = false;
        }
    }

    public void PauseGame(bool dead)
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;

        if (dead)
        {
            Restart.SetActive(true);
            DeadText.SetActive(true);
            Resume.SetActive(false);
            PausedText.SetActive(false);
            WinText.SetActive(false);
        }
        else
        {
            Resume.SetActive(true);
            PausedText.SetActive(true);
            Restart.SetActive(false);
            DeadText.SetActive(false);
            WinText.SetActive(false);

        }
    }

    public void OnResumeButton()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1;
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    public void OnQuitToMainMenu()
    {
        SceneManager.LoadScene("Title screen");
    }

    public void OnQuitToDesktop()
    {
        Debug.Log("Game has Quit");
        Application.Quit();
    }

}
