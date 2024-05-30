using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWin : MonoBehaviour
{
    //Ref�rence � scene fader 
    public SceneFader sceneFader;
    //Source audio 
    public AudioSource buttons;
    //clip audio 
    public AudioClip buttonClick;
    //le nom de la scene � charger 
    public string nextLevel;

    //Niveau � charger 
    public void LevelToLoad()
    {
        //On joue le son au click sur le bouton
        buttons.PlayOneShot(buttonClick);
        //On sauvegarde le niveau d�bloqu� 
        PlayerPrefs.SetString("levelReached", nextLevel);
        sceneFader.FadeTo(nextLevel);
    }
}
