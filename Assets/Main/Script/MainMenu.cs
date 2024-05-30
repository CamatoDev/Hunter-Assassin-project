using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Reférence à scene fader 
    public SceneFader sceneFader;
    //Text du niveau à jouer 
    public Text levelToLoad;
    //Niveau à jouer
    string levelReached;

    //Source audio 
    public AudioSource buttons;
    //clip audio 
    public AudioClip buttonClick;

    private void Start()
    {
        //Création de la variable de sauvegarde de niveau (qui sera concervé même après la fermeture du jeu) 
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

    //fonction pour lancé le menu des améliorations
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
