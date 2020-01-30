using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float delay = 3f;

    public GameObject explosionEffect;

    float countDown;

    bool hasExploded = false;


    
    
    
    // Start is called before the first frame update
    void Start()
    {
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;

        if (countDown <= 0.0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explosion, 1.5f);


    }

}
