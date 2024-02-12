using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneHit : MonoBehaviour

{
    public GooberController gooberController; // Reference to the GooberController script

    // This method is called when the UI Button is clicked
    public void OnButtonClick()
    {
        // Ensure GooberController reference is assigned before attempting to use it
        if (gooberController != null)
        {
            // Activate GodMode
            gooberController.oneShot = true;
        }
        else
        {
            Debug.LogError("GooberController reference is not set in the Inspector!");
        }
    }
}