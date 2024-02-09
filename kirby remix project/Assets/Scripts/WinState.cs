using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    public GameObject Win_Screen;
    public GameObject Goober;
    public AudioSource YouWinSound;
    // Level move zoned enter, if collider is a player

    private void OnTriggerEnter2D(Collider2D other) {
        print("Trigger Entered");
        
        // Could use other.GetComponent<Player>() to see if the game object has a Player component
        // Tags work too. Maybe some players have different script components?
        if(other.tag == "Player") 
        {
            Win_Screen.SetActive(true);  //activates the lose screen when goober falls on the collider.
            Goober.SetActive(false); //deletes goober.
            YouWinSound.Play();

        }
    }

}