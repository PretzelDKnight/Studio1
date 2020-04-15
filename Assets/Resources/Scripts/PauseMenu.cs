using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused;

    public GameObject pauseUI;

    UnityEngine.SceneManagement.LoadSceneMode mode;

    private void Start()
    {
        isPaused = false;
        pauseUI.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
                isPaused = true;
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
        isPaused = false;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseUI.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.LogError("QUITTING APP!");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Scheletro", mode = LoadSceneMode.Single);
    }

    public void PartySelect()
    {
        //PartySelect menu
        Debug.LogError("Party Selection!");
    }

    public void OptionsMenu()
    {
        //Options menu
        Debug.LogError("Option Menu!");
    }
}
