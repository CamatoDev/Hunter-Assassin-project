using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    //r�f�rence � GameManager 
    public GameManager manager;
    //nombre de vie du joueur 
    public static float playerLive;
    //variable qui indique si le joueur est mort ou pas 
    bool isDeath = false;
    //Nombre de piece du joueur 
    public int playerGain = 0;
    /*Pens� � faire une variable pour les pi�ce global qui servira aux achats in game du joueur (Nouveau personnage)*/
    //Nombre d'enemi tuer 
    public float playerKillNomber = 0f;
    public Text killNomber;
    //Nombre de temps pass� dans le niveau 
    public static float playerTime;
    public Text timer;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //Initialisation de la vie 
        playerLive = 100f; 
        playerGain = 0;
        //Le temps du que le joueur dois passer dans le niveau est d�fini  
        playerTime = manager.Leveltime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDeath)
        {
            //Le temps que le joueur passe est incr�ment� � chaque seconde 
            playerTime -= Time.deltaTime;
            //On bloque la valeur du temps � 0
            if(playerTime <= 0f)
                playerTime = 0f;
            //on actualise le temps pass� dans le niveau 
            timer.text = string.Format("{0:00.00}", playerTime); 
        }

        //on actualise le nombre de d'ennemi tu�
        killNomber.text = playerKillNomber.ToString() + " / " + GameManager.enemyNomber.ToString();

        //si le nombre d'ennemi tuer est �gal au nombre d'�nnemi dans la sc�ne le niveau est termin�
        if(playerKillNomber == GameManager.enemyNomber)
        {
            Debug.Log("Level Won !");
            manager.WinLevel();
            //on d�sactive le script pour ne pas qu'il soit lu par la suite
            this.enabled = false;
        }
    }

    //fonction de recompense par kill
    public void Gain(int Value)
    {
        //Le joueur re�oit une recompense apr�s chaque ennemi tuer 
        playerGain += Value;
        Debug.Log("Le joueur re�oit : " + Value + " points de recompense !");
        //Le nombre global de pi�ce du joueur est �gale au nombre de pi�ce pr�c�dent ajout� au nouveau gain
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + Value);
    }

    //Fonction qui permet au joueur de prendre des d�gats 
    public void TakeDamage(float TheDamage)
    {
        if(!isDeath) //si le joueur n'est pas mort 
        {
            //appliquer les d�gats au joueur 
            playerLive -= TheDamage;
            Debug.Log("Le joueur a re�u " + TheDamage + " points de d�gats.");

            if(playerLive <= 0f) //si la vie du joueur est inf�rieur ou �gale � z�ro 
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
