using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthBoost : MonoBehaviour
{
    //Bouton d'activation 
    public Button healthBoost;
    //Distance d'activation 
    public float range = 0.5f;
    //Joueur 
    public Transform player;
    //Le boost a été utilisé
    private bool use;

    // Start is called before the first frame update
    void Start()
    {
        use = false;
        //le bouton n'est pas visible au lancement 
        healthBoost.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Distance entre le boost et le bouton 
        float distanceToboost = Vector3.Distance(transform.position, player.position);
        //On vérifie si la distance est plus petite que la distance d'activation 
        if(distanceToboost <= range && !use)
        {
            //Si c'est le cas on active le bouton pour prendre le boost de vie
            healthBoost.gameObject.SetActive(true);
        }
        else
        {
            //On desactive le bouton 
            healthBoost.gameObject.SetActive(false);
        }
    }

    //fonction de boost de vie 
    public void Boost()
    {
        use = true;
        //On desactive le bouton 
        healthBoost.gameObject.SetActive(false);
        //Le joueur reste sur place quand il se soigne 
        player.gameObject.GetComponent<NavMeshAgent>().SetDestination(player.position);
        //Si la vie du joueur est plus basse que 100
        if (PlayerStats.playerLive < 60f)
        {
            //La vie du joueur repasse à 100
            PlayerStats.playerLive = 60f;
        }
        //On detruit le boost après 2 seconde 
        Destroy(gameObject, 1f);
        //Destroy(healthBoost.gameObject, 3f);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
