using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //R�f�rence au Script FieldOfView
    FieldOfView FieldOfView;
    //Source Audio 
    public AudioSource enemySound;
    //Audio Clip 
    public AudioClip gunSound;
    //La variable qui vas contenir le joueuer (cible de l'enemi) 
    public Transform target;
    //La valeur de l'ennemi 
    public int value = 50;

    //Distance entre le joueuer et l'enemi 
    float Distance;
    //vitesse � laquelle la poursuite s'active 
    public float chaseSpeed = 1f;
    public float securityDistance = 0.2f;


    /*penser � un coldown entre le fin de la poursuite et le retour � la patrouille normal*/
    public float escapeDistance = 1.1f;


    //Variable pour tirer sur le joueuer 
    public float fireRate = 1f;
    float fireCountDown = 0f;
    public GameObject arrowPrefab;
    public Transform firePoint;
    public Transform eyes;

    //Pour la recgerche de l'enemy
    public float patrolingSpeed = 0.8f;
    bool walkPointSet;
    public float walkPointRange = 1.5f;
    Vector3 walkPoint;
    //variable qui dis si l'ennemi est � la poursuit du joueur ou non
    bool isFollowPlayer = false;
    //Pour les animation 
    public Animator animator;
    //Pour le contr�le de l'animator 
    float enemyState;

    public NavMeshAgent agent;

    //
    private void Start()
    {
        //R�cup�re le script 
        FieldOfView = gameObject.GetComponent<FieldOfView>();
        //Recup�re le component de l'audio source 
        enemySound = gameObject.GetComponent<AudioSource>();
        //Recuperer le component des animations 
        animator = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(transform.position, target.position);

        if (!FieldOfView.canSeePlayer)
        {
            Patroling();
        }
        
        //L'enemi poursuit le joueuer en lui tirant dessus 
        if (FieldOfView.canSeePlayer && Distance > securityDistance)
        {
            Chase();
        }

        //l'enemie s'arr�te � une distance de s�curit� et tire sur le joueuer
        if (FieldOfView.canSeePlayer && Distance <= securityDistance)
        { 
            agent.destination = transform.position;
            transform.LookAt(target);
            //Tire 
            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }

            fireCountDown -= Time.deltaTime;
        }

        animator.SetFloat("State", enemyState);
    }

    void Patroling()
    {
        enemyState = 0.3f;
        //Simple patrouille
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            //On fixe la destination de l'ennemi 
            agent.destination = walkPoint;

            //Si l'ennemi ne poursuit le pas le joueur 
            if(!isFollowPlayer)
            {
                agent.speed = patrolingSpeed;
            }
            else //Si il le poursuit 
            {
                agent.speed = chaseSpeed;
            }

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Une fois arriv� au point de patrouille 
            if(distanceToWalkPoint.magnitude < 0.8f)
            {
                //On supprime l'ancien point de patrouille 
                walkPointSet = false;
                isFollowPlayer = false;
            }
        }
    }

    //fonction pour ce diriger vers une zone de bruit 
    public void FindPlayer(Vector3 point)
    {
        //la poursuite debute 
        isFollowPlayer = true;
        //on definit le point vers lequel le joueur a �t� rep�rer 
        walkPoint = point;
    }

    //Chercher le point vers lequel l'enemi patrouille 
    void SearchWalkPoint()
    {
        //Calcule du point de patrouille 
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up))
        {
            walkPointSet = true;
        }
    }

    //Quand le joueuer est detect� 
    void Chase()
    {
        enemyState = 0.6f;
        agent.destination = target.transform.position;
        agent.speed = chaseSpeed;
    }

    void Shoot()
    {
        enemyState = 1f;
        enemySound.PlayOneShot(gunSound);
        Debug.Log("Tir effectu�.");
        GameObject arrowtGO = (GameObject)Instantiate(arrowPrefab, firePoint.position, eyes.rotation);
        //ref�rence au script bullet
        Bullets arrow = arrowtGO.GetComponent<Bullets>();

        //v�rification que le script n'est pas null
        if(arrow != null)
        {
            arrow.Seek(target);
        }
    }
}
