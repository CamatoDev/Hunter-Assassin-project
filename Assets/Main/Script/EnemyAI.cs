using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //Source Audio 
    public AudioSource enemySound;
    //Audio Clip 
    public AudioClip gunSound;
    //La variable qui vas contenir le joueuer (cible de l'enemi) 
    public Transform target;
    //La valeur de l'ennemi 
    public int value = 20;

    //Distance entre le joueuer et l'enemi 
    float Distance;
    //distance à laquelle la poursuite s'active 
    public float chaseRange = 1f;
    public float chaseSpeed = 1f;
    public float securityDistance = 0.2f;


    /*penser à un coldown entre le fin de la poursuite et le retour à la patrouille normal*/
    public float escapeDistance = 1.1f;


    //Variable pour tirer sur le joueuer 
    public float fireRate = 1f;
    float fireCountDown = 0f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform eyes;

    //Pour la recgerche de l'enemy
    public float patrolingSpeed = 0.8f;
    bool walkPointSet;
    public float walkPointRange = 1.5f;
    Vector3 walkPoint;
    //variable qui dis si l'ennemi est à la poursuit du joueur ou non
    bool isFollowPlayer = false;
    //Pour les animation 
    public Animator animator;

    public NavMeshAgent agent;

    //
    private void Start()
    {
        //Recupère le component de l'audio source 
        enemySound = GetComponent<AudioSource>();
        //Recuperer le component des animations 
        animator = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Distance = Vector3.Distance(transform.position, target.position);

        if (Distance > chaseRange)
        {
            Patroling();
        }
        
        //L'enemi poursuit le joueuer en lui tirant dessus 
        if (Distance < chaseRange && Distance > securityDistance)
        {
            Chase();
        }

        //l'enemie s'arrête à une distance de sécurité et tire sur le joueuer
        if (Distance < securityDistance)
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
    }

    void Patroling()
    {
        //Simple patrouille
        if(!walkPointSet)
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
                animator.SetFloat("Run", 0f);
                agent.speed = patrolingSpeed;
            }
            else //Si il le poursuit 
            {
                animator.SetFloat("Run", 1f);
                agent.speed = chaseSpeed;
            }

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            //Une fois arrivé au point de patrouille 
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
        //on definit le point vers lequel le joueur a été repérer 
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

    //Quand le joueuer est detecté 
    void Chase()
    {
        animator.SetFloat("Run", 1f);
        agent.destination = target.transform.position;
        agent.speed = chaseSpeed;

        ////Tire 
        //if(fireCountDown <= 0f)
        //{
        //    Shoot();
        //    fireCountDown = 1f / fireRate; 
        //}

        //fireCountDown -= Time.deltaTime;
    }

    void Shoot()
    {
        enemySound.PlayOneShot(gunSound);
        animator.SetBool("Shoot", true);
        Debug.Log("Tir effectué.");
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, eyes.rotation);
        //reférence au script bullet
        Bullets bullet = bulletGO.GetComponent<Bullets>();

        //vérification que le script n'est pas null
        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        //Gizmos.DrawIcon();
        //Gizmos.DrawFrustum(transform.position, fov, maxRange, minRange, aspect);
    }
}
