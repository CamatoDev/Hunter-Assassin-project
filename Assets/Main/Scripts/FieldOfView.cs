using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    //Rayon de recherche 
    public float radius;
    [Range(0,360)]
    //angle de la zone de recherche dans le rayon 
    public float angle;

    //Variable pour le joueur 
    public GameObject playerRef;

    //Layers Mask de la cible et des obstacle à ne pas traverser 
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    //Variable qui définit la visibilité du joueur par la cible 
    public bool canSeePlayer;

    private void Start()
    {
        //On récupère le joueur 
        playerRef = GameObject.FindGameObjectWithTag("Player");
        //On lance la coroutine de verification  
        StartCoroutine(FOVRoutine());
    }
 
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    //fonction de recherche du joueur 
    private void FieldOfViewCheck()
    {
        //On trace un cercle autour de l'ennemi dans lequel sera définit la zone de recherche et qui renvoi le objet ayant le LayerMask définit dans un tableau
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        //Si le nombre d'élément du tableau n'est pas nul
        if (rangeChecks.Length != 0)
        {
            //le premier objet de ce tableau sera notre cible 
            Transform target = rangeChecks[0].transform;
            //On récupère la diection entre la cible et l'ennemi et on la normalize  
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                //Distance entre l'ennemi et la cible (le joueur)
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                //On trace un ligne dont on vérifie si elle ne touche pas d'obstacle en allant jusqu'au jouer 
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    //Le joueur est visible 
                    canSeePlayer = true;
                else
                    //Le joueur n'est pas visible 
                    canSeePlayer = false;
            }
            else
                //Le joueur n'est pas visible
                canSeePlayer = false;
        }
        //Si il n'y a pas de joueur dans la zone de recherche et que le joueur était visible avant 
        else if (canSeePlayer)
            //Le joueur n'est plus visible
            canSeePlayer = false;
    }
}
