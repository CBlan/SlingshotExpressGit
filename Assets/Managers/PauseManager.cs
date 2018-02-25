using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Switches Pause state of the game, including main menu hide/unhide and time freeze

public class PauseManager : MonoBehaviour
{
    public bool isPaused = false;

    public KeyCode pauseKey = KeyCode.Escape;

    public GameObject pausePanel;

    private void Start()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            PauseGame();
        }

    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Cursor.visible = isPaused;
            Time.timeScale = 0.0f;
            pausePanel.SetActive(isPaused);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = isPaused;
            Time.timeScale = 1.0f;
            pausePanel.SetActive(isPaused);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}