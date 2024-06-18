using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWin : MonoBehaviour
{
    //Reférence à scene fader 
    public SceneFader sceneFader;
    //Source audio 
    public AudioSource buttons;
    //clip audio 
    public AudioClip buttonClick;
    //le nom de la scene à charger 
    public string nextLevel;

    //Niveau à charger 
    public void LevelToLoad()
    {
        //On joue le son au click sur le bouton
        buttons.PlayOneShot(buttonClick);
        //On sauvegarde le niveau débloqué 
        PlayerPrefs.SetString("levelReached", nextLevel);
        sceneFader.FadeTo(nextLevel);
    }
}
