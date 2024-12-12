using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PausePressed()
    {
        if (isPaused) ResumeGame();
        else PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
