using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewards : MonoBehaviour
{
    private Transform target;
    //vitesse de la recompense
    public float speed = 70f;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        //si la récompense n'as pas/plus de cible elle doit disparaitre 
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        //Direction de la balle 
        Vector3 dir = target.position - transform.position;
        //De combien on ce deplace dans cette frame 
        float distanceThisFrame = speed * Time.deltaTime;

        //si on touche l'enemi 
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        //on deplace la recompense 
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Debug.Log("Recompense obtenu");
        Destroy(gameObject);
    }
}
