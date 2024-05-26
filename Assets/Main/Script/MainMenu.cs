using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Text du niveau à jouer 
    public Text levelToLoad;

    //Source audio 
    public AudioSource buttons;
    //clip audio 
    public AudioClip buttonClick;

    private void Start()
    {
        //Création de la variable de sauvegarde de niveau (qui sera concervé même après la fermeture du jeu) 
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        levelToLoad.text = "Level " + levelReached;
    }

    //Fonction pour jouer 
    public void PlayLevel()
    {
        //On joue le son au click sur le bouton
        buttons.PlayOneShot(buttonClick);
        SceneManager.LoadScene("Level1");
    }

    //fonction pour lancé le menu des améliorations
    public void Upgrade()
    {
        //On joue le son au click sur le bouton
        buttons.PlayOneShot(buttonClick);
        SceneManager.LoadScene("UpgradeMenu");
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
