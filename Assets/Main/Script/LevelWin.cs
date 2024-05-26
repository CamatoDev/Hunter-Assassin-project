using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWin : MonoBehaviour
{
    //r�f�rence au GameManager
    public GameManager gameManager;
    //le nom de la scene � charger 
    public string nextLevel;
    //l'indice de la sc�ne � charger 
    public int levelToUnlock;

    //Niveau � charger 
    public void LevelToLoad()
    {
        //On sauvegarde le niveau d�bloqu� 
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        SceneManager.LoadScene(nextLevel);
    }
}
