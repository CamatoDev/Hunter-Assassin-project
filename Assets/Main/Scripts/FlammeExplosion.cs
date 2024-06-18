using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FlammeExplosion : MonoBehaviour
{
    //Bouton d'activation 
    public Button bombBonus;
    //Distance d'activation 
    public float range = 0.25f;
    //Rayon de l'explosion 
    public float explosionRadius = 2f;
    //temps avant l'explosion
    public int timer = 2;
    //Joueur 
    public Transform player;
    //Le boost a été utilisé
    private bool use;
    //Source audio 
    AudioSource audioSource;
    //Audio clip
    public AudioClip activated;
    public AudioClip explosion;

    // Start is called before the first frame update
    void Start()
    {
        use = false;
        //le bouton n'est pas visible au lancement 
        bombBonus.gameObject.SetActive(false);
        //On recupère la source audio
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Distance entre le boost et le bouton 
        float distanceToboost = Vector3.Distance(transform.position, player.position);
        //On vérifie si la distance est plus petite que la distance d'activation 
        if (distanceToboost <= range && !use)
        {
            //Si c'est le cas on active le bouton pour prendre le boost de vie
            bombBonus.gameObject.SetActive(true);
            use = false;
        }
        else
        {
            //On desactive le bouton 
            bombBonus.gameObject.SetActive(false);
        }
    }

    //fonction de boost de vie 
    public void Boost()
    {
        use = true;
        //On lance le son d'activation 
        audioSource.PlayOneShot(activated);
        //On desactive le bouton 
        bombBonus.gameObject.SetActive(false);
        //Le joueur reste sur place quand il active la bomb
        player.gameObject.GetComponent<NavMeshAgent>().SetDestination(player.position);
        //Le ennemis se dirige vers le bruit
        //player.gameObject.GetComponent<Player_Nav>().PlayerDetected();
        //On lance l'explosion
        StartCoroutine(Explode());
        //On detruit le boost après 2 seconde 
        Destroy(gameObject, 2.5f);
    }

    //Couroutine pour l'explosion 
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(timer);
        //On recupère tout les colliders dans un rayon définit 
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        //On joue le son de l'explosion
        audioSource.PlayOneShot(explosion);
        foreach (Collider collider in colliders)
        {
            //Si le collider à le tag Enemy 
            if(collider.tag == "Enemy")
            {
                //On appelle la fonction Dead de l'ennemi 
                collider.gameObject.GetComponent<EnemyAI>().Dead();
            }
            if(collider.tag == "Player")
            {
                //On appelle la fonction Dead du joueur 
                collider.gameObject.GetComponent<PlayerStats>().Dead();
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
