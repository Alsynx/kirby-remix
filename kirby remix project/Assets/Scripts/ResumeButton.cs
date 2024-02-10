using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    // Reference to the GameManager script
        public GameObject pausemenu;

    // This method is called when the Resume button is clicked
    public void OnButtonClick()
    {
        // Call the method to resume the game from the GameManager script
            Time.timeScale = 1f;
            pausemenu.SetActive(false);
    }
}