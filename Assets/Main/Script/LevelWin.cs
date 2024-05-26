using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWin : MonoBehaviour
{
    //référence au GameManager
    public GameManager gameManager;
    //le nom de la scene à charger 
    public string nextLevel;
    //l'indice de la scène à charger 
    public int levelToUnlock;

    //Niveau à charger 
    public void LevelToLoad()
    {
        //On sauvegarde le niveau débloqué 
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        SceneManager.LoadScene(nextLevel);
    }
}
