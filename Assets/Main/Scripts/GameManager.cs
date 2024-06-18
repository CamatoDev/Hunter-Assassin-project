using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Nombre total d'enemi dans le niveau 
    public static float enemyNomber;
    string enemyTag = "Enemy";
    //Nombre de temps prévu pour le niveau 
    public float Leveltime = 100f;
    //menu de gameover 
    public GameObject gameOver;
    public static bool gameIsOver;
    //menu de victoire 
    public GameObject levelWin;
    

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        enemyNomber = enemies.Length;
        gameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
        {
            return;
        }

        //si les vie du joueur sont à zéro
        if (PlayerStats.playerLive <= 0f)
        {
            GameOver();
        }
        //si le temps arrive à 0
        if(PlayerStats.playerTime <= 0f)
        {
            GameOver();
        }
    }

    //fonction pour la défaite 
    public void GameOver()
    {
        gameIsOver = true;
        gameOver.SetActive(true);
    }

    //fonction pour la victoire 
    public void WinLevel()
    {
        gameIsOver = true;
        levelWin.SetActive(true);
    }
}
