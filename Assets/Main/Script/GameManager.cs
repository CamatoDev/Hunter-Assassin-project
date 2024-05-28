using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Nombre total d'enemi dans le niveau 
    public static float enemyNomber;
    string enemyTag = "Enemy";
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

        //si les vie du joueur sont � z�ro
        if (PlayerStats.playerLive <= 0f)
        {
            GameOver();
        }
        //si le temps arrive � 0
        if(PlayerStats.playerTime <= 0f)
        {
            GameOver();
        }
    }

    //fonction pour la d�faite 
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
