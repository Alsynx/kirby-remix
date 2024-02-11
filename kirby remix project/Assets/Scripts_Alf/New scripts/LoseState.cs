using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseState : MonoBehaviour
{
    public GameObject Lose_Screen;
    public GameObject Goober;
    public AudioSource audioSource;
    public AudioClip YouLoseSound;
    // Level move zoned enter, if collider is a player

    private void OnTriggerEnter2D(Collider2D other) {
        print("Trigger Entered");
        
        // Could use other.GetComponent<Player>() to see if the game object has a Player component
        // Tags work too. Maybe some players have different script components?
        if(other.tag == "Player") 
        {
            Lose_Screen.SetActive(true);  //activates the lose screen when goober falls on the collider.
            Goober.SetActive(false); //deletes goober.
            PlaySound(YouLoseSound);

        }
    }

    public void GameOver()
    {
        
        Lose_Screen.SetActive(true);  //activates the lose screen when goober falls on the collider.
        Goober.SetActive(false); //deletes goober.
        PlaySound(YouLoseSound);
        
    }

    //Made a few adjustments for Audio Clip config -Alfred
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
