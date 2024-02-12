using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    public GameObject pausemenu;

public void OnButtonClick()
{
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
} 
}