using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
   public void Retry()
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

    //Retourner au menu 
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
