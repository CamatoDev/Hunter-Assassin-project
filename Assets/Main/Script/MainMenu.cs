using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Ref�rence � scene fader 
    public SceneFader sceneFader;
    //Text du niveau � jouer 
    public Text levelToLoad;
    //Niveau � jouer
    string levelReached;

    //Source audio 
    public AudioSource buttons;
    //clip audio 
    public AudioClip buttonClick;

    private void Start()
    {
        //Cr�ation de la variable de sauvegarde de niveau (qui sera concerv� m�me apr�s la fermeture du jeu) 
        PlayerPrefs.SetString("levelReached", "Level1");
        levelReached = PlayerPrefs.GetString("levelReached");

        levelToLoad.text = levelReached;
    }

    //Fonction pour jouer 
    public void PlayLevel()
    {
        //On joue le son au click sur le bouton
        buttons.PlayOneShot(buttonClick);
        sceneFader.FadeTo(levelReached);
    }

    //fonction pour lanc� le menu des am�liorations
    public void Upgrade()
    {
        //On joue le son au click sur le bouton
        buttons.PlayOneShot(buttonClick);
        sceneFader.FadeTo("UpgradeMenu");
    }

    //fonction pour quitter le jeu 
    public void Quit()
    {
        //On joue le son au click sur le bouton
        buttons.PlayOneShot(buttonClick);
        Debug.Log("Fermeture du jeu !");
        Application.Quit();
    }
}
