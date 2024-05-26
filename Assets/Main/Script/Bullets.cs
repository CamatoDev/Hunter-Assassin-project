using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private Transform target;
    //vitesse de la balle
    public float speed = 70f;
    //dégat de la balle 
    public float damage = 10f;
    //effect de la balle 
    public GameObject bulletImpactEffect;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        //si la balle n'as pas/plus de cible elle doit disparaitre 
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        //Direction de la balle 
        Vector3 dir = target.position - transform.position;
        //De combien on ce deplace dans cette frame 
        float distanceThisFrame = speed * Time.deltaTime;

        //si on touche l'enemi 
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        //on deplace la balle 
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        //transform.Translate(Vector3.forward * speed); Recherche sur le tire sur un ennemi
    }

    void HitTarget()
    {
        Debug.Log("Balle contact !");
        GameObject effectGO = (GameObject)Instantiate(bulletImpactEffect, transform.position, transform.rotation);
        Destroy(effectGO, 1f);
        Destroy(gameObject);
        //Destroy(target.gameObject);
        target.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
