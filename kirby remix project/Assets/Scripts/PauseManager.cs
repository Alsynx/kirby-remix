using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pausemenu;

    void Update()
    {
        // Check for input to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        // Toggle the pause state
        isPaused = !isPaused;

        // If the game is paused, set the timescale to 0
        if (isPaused)
        {
            Time.timeScale = 0f;
            Debug.Log("Game Paused");
            pausemenu.SetActive(true);
            // You can add additional pause functionality here (e.g., showing a pause menu)
        }
        else
        {
            // If the game is unpaused, set the timescale back to 1
            Time.timeScale = 1f;
            Debug.Log("Game Unpaused");
             pausemenu.SetActive(false);
            // You can add additional unpause functionality here
        }
    }
}