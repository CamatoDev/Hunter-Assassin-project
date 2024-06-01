using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    //référence à GameManager 
    public GameManager manager;
    //nombre de vie du joueur 
    public static float playerLive;
    //variable qui indique si le joueur est mort ou pas 
    bool isDeath = false;
    //Nombre de piece du joueur 
    public int playerGain = 0;
    /*Pensé à faire une variable pour les pièce global qui servira aux achats in game du joueur (Nouveau personnage)*/
    //Nombre d'enemi tuer 
    public float playerKillNomber = 0f;
    public Text killNomber;
    //Nombre de temps passé dans le niveau 
    public static float playerTime;
    public Text timer;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //Initialisation de la vie 
        playerLive = 100f; 
        playerGain = 0;
        //Le temps du que le joueur dois passer dans le niveau est défini  
        playerTime = manager.Leveltime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDeath)
        {
            //Le temps que le joueur passe est incrémenté à chaque seconde 
            playerTime -= Time.deltaTime;
            //On bloque la valeur du temps à 0
            if(playerTime <= 0f)
                playerTime = 0f;
            //on actualise le temps passé dans le niveau 
            timer.text = string.Format("{0:00.00}", playerTime); 
        }

        //on actualise le nombre de d'ennemi tué
        killNomber.text = playerKillNomber.ToString() + " / " + GameManager.enemyNomber.ToString();

        //si le nombre d'ennemi tuer est égal au nombre d'énnemi dans la scène le niveau est terminé
        if(playerKillNomber == GameManager.enemyNomber)
        {
            Debug.Log("Level Won !");
            manager.WinLevel();
            //on désactive le script pour ne pas qu'il soit lu par la suite
            this.enabled = false;
        }
    }

    //fonction de recompense par kill
    public void Gain(int Value)
    {
        //Le joueur reçoit une recompense après chaque ennemi tuer 
        playerGain += Value;
        Debug.Log("Le joueur reçoit : " + Value + " points de recompense !");
        //Le nombre global de pièce du joueur est égale au nombre de pièce précédent ajouté au nouveau gain
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + Value);
    }

    //Fonction qui permet au joueur de prendre des dégats 
    public void TakeDamage(float TheDamage)
    {
        if(!isDeath) //si le joueur n'est pas mort 
        {
            //appliquer les dégats au joueur 
            playerLive -= TheDamage;
            Debug.Log("Le joueur a reçu " + TheDamage + " points de dégats.");

            if(playerLive <= 0f) //si la vie du joueur est inférieur ou égale à zéro 
            {
                Dead(); //on lance la fonction qui tue le joueur 
            }
        }
    }

    //fonction pour la mort du joueur 
    void Dead()
    {
        Debug.Log("Le joueur est mort.");
        Destroy(gameObject);
    }
}
