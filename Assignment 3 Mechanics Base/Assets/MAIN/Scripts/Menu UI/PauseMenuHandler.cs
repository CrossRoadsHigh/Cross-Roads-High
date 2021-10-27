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
    public GameObject PausedText;


    private bool isPaused = false;


    void Start()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
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
        }
        else
        {
            Resume.SetActive(true);
            PausedText.SetActive(true);
            Restart.SetActive(false);
            DeadText.SetActive(false);
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
