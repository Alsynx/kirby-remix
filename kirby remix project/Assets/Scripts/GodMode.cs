using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodMode : MonoBehaviour
{
    // Reference to the GameManager script
        public GameObject optionsmenu;

    // This method is called when the Resume button is clicked
    public void OnButtonClick()
    {
        // Call the method to resume the game from the GameManager script
            GooberController gm = this.GetComponent<GooberController>();
            gm.GodModeOn = true;
            optionsmenu.SetActive(false);
    }
}
