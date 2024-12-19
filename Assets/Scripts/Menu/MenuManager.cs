using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject missionMenu;


    public void PausePressed()
    {
        if (isPaused) ResumeGame();
        else PauseGame(pauseMenu);
    }
    public void MissionMenuPressed()
    {
        if (isPaused) ResumeGame();
        else PauseGame(missionMenu);
    }

    public void PauseGame(GameObject menu)
    {
        Time.timeScale = 0;
        menu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        missionMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
