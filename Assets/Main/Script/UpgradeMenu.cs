using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeMenu : MonoBehaviour
{
    //Reférence à scene fader 
    public SceneFader sceneFader;
    //Source audio 
    public AudioSource buttons;
    //clip audio 
    public AudioClip buttonClick;
    //Text du montant du joueur 
    public Text money;

    //
    private void Start()
    {
        //on sauvergarde l'argent du joueur 
        if(PlayerPrefs.GetInt("Money") <= 0)
        {
            PlayerPrefs.SetInt("Money", 0);
        }

        money.text =  PlayerPrefs.GetInt("Money").ToString();

    }

    //Fonction pour choisir le joueur 
    public void SelectionPlayer()
    {
        //On joue le son au click sur le bouton 
        buttons.PlayOneShot(buttonClick);
    }

    //fonction pour sortir du menu des améliorations
    public void Menu()
    {
        buttons.PlayOneShot(buttonClick);
        sceneFader.FadeTo("MainMenu");
    }
}
