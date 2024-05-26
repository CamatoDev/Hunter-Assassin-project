using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_Nav : MonoBehaviour
{
    //R�f�rence au script PlayerStat
    PlayerStats Player;
    //Source audio 
    public AudioSource playSound;
    //Audio clip
    public AudioClip attackSound;
    public NavMeshAgent agent;
    public float offSet = 3.5f;

    public Transform target;
    string enemyTag = "Enemy";
    //Distance entre le joueuer et l'enemi 
    float Distance;
    //Distance d'attaque du joueuer 
    public float attackDistance = 0.15f;
    public float targetLoockDist = 0.1f;
    //recompense 
    public GameObject rewardsPrefab;
 

    public Camera Camera;

    private void Start()
    {
        InvokeRepeating("UpdateEnemy", 0f, 0.5f);

        //recuperer le script 
        Player = gameObject.GetComponent<PlayerStats>();
        //Recuperer le component audio source du joueur 
        playSound = gameObject.GetComponent<AudioSource>();
    }

    //Mettre � jour les enemies 
    void UpdateEnemy()
    {
        //cr�ation d'un tableau d'enemi 
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        //Distance entre le jouers et chaque enemie 
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            GameObject nearestEnemy = null;
            //si la distance est plus petite que la plus petiti distance 
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            if(nearestEnemy != null)
            {
                target = nearestEnemy.transform;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //v�rification du nombre d'enemi restant dans la sc�ne 
        if (target == null)
        {
            return;
        }

        //distanc entre le jouer et sa cible 
        Distance = Vector3.Distance(transform.position, target.position);
        //positionnement de la camera 
        Camera.transform.position = new Vector3(transform.position.x, offSet, transform.position.z);
        if(Input.touchCount > 0)
        {
            Ray ray = Camera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                float distClick_target = Vector3.Distance(hit.transform.position, target.position);
                //on v�rifie si je joueur � cliqu� sur une enemy
                if (distClick_target <= targetLoockDist)
                {
                    //si c'est le cas on verouille l'ennemy 
                    Debug.Log("Click on enemy");
                    /*modifi� pour que je joueur suive l'ennemi dans ces d�placement jusqu'� le tuer et une fois l'ennemi mort faire spwan les r�compenses*/
                    agent.destination = target.position; 
                }
                else
                {
                    //si ce n'est pas le cas on ce dirige simplement � l'endroit indiqu� 
                    agent.SetDestination(hit.point);
                }
            }
        }

        //pour les test � la souris 
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                float distClick_target = Vector3.Distance(hit.transform.position, target.position);
                //on v�rifie si je joueur � cliqu� sur une enemy
                if (distClick_target <= targetLoockDist)
                {
                    //si c'est le cas on verouille l'ennemy 
                    Debug.Log("Click on enemy");
                    /*modifi� pour que je joueur suive l'ennemi dans ces d�placement jusqu'� le tuer et une fois l'ennemi mort faire spwan les r�compenses*/
                    agent.SetDestination(target.transform.position); 
                }
                else
                {
                    //si ce n'est pas le cas on ce dirige simplement � l'endroit indiqu� 
                    agent.SetDestination(hit.point);
                }
            }
        }//fin de la partie de test � la souris
         
        //Attaquer l'enemi si il est � une bonne distance 
        if (Distance <= attackDistance)
        {
            Attack();
            PlayerDetected();
        }
    }

    /*public void OnColisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(enemyTag))
        {
            Debug.Log("Enemi detruit.");
            Destroy(collision.transform.gameObject);
        }
    }*/

    void Attack()
    {
        playSound.PlayOneShot(attackSound);
        Rewards();
        Destroy(target.gameObject);
        Debug.Log("Enemi detruit.");
        Player.playerKillNomber += 1;
        Player.Gain(target.gameObject.GetComponent<EnemyAI>().value);
    }

    void PlayerDetected()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyAI>().FindPlayer(transform.position);
        }
    }

    //fonction pour le spawn de la recompense 
    void Rewards()
    {
        Debug.Log("Rewards spawn !");
        GameObject rewardGO = (GameObject)Instantiate(rewardsPrefab, target.position, target.rotation);
        GameObject rewardGO1 = (GameObject)Instantiate(rewardsPrefab, target.position, target.rotation);
        GameObject rewardGO2 = (GameObject)Instantiate(rewardsPrefab, target.position, target.rotation);
        GameObject rewardGO3 = (GameObject)Instantiate(rewardsPrefab, target.position, target.rotation);
        GameObject rewardGO4 = (GameObject)Instantiate(rewardsPrefab, target.position, target.rotation);
        //ref�rence au script bullet
        GameObject[] rewards = GameObject.FindGameObjectsWithTag("Rewards");
        foreach (GameObject reward in rewards)
        {
            Rewards s_reward = reward.GetComponent<Rewards>();
            //v�rification que le script n'est pas null
            if (s_reward != null)
            {
                s_reward.Seek(target);
            }
        }
    }
}


//public void OnCollisionEnter(Collision collision)
//{
//    if (collision.transform.CompareTag("Enemy"))
//    {
//        Destroy(collision.gameObject);
//        Debug.Log("Enemi detruit.");
//    }
//}
//public void OnTriggerEnter(Collider collision)
//{
//    if (collision.transform.CompareTag("Enemy"))
//    {
//        Destroy(collision.gameObject);
//        Debug.Log("Enemi detruit.");
//    }
//}