using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //Reférence à scene fader 
    public SceneFader sceneFader;
    //Source audio 
    public AudioSource buttons;
    //clip audio 
    public AudioClip buttonClick;
    public void Retry()
    {
        //On joue le son au click sur le bouton
        buttons.PlayOneShot(buttonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Retourner au menu 
    public void MainMenu()
    {
        //On joue le son au click sur le bouton
        buttons.PlayOneShot(buttonClick);
        sceneFader.FadeTo("MainMenu");
    }
}
