using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMoveMain : MonoBehaviour
{
    public int sceneBuildIndex;
    public GameObject pausemenu;


    public void OnButtonClick()
    {
        // Call the method to resume the game from the GameManager script
            Time.timeScale = 1f;
            pausemenu.SetActive(false);
    }

}
