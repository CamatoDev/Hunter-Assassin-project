using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Text du niveau � jouer 
    public Text levelToLoad;

    //Source audio 
    public AudioSource buttons;
    //clip audio 
    public AudioClip buttonClick;

    private void Start()
    {
        //Cr�ation de la variable de sauvegarde de niveau (qui sera concerv� m�me apr�s la fermeture du jeu) 
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

    //fonction pour lanc� le menu des am�liorations
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
