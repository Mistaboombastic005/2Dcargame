using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject pauseButton;
    
    public void Update()
    {
        if (PauseButton.paused)
        {
            menu.SetActive(true);
            pauseButton.SetActive(false);
        }
        else
        {
            menu.SetActive(false);
        }
    }

    public void LeaveToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        PauseButton.paused = false;
    }

    public void LeaveToGarage()
    {
        SceneManager.LoadScene("Garage");
    }
}
