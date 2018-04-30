using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileNade : MonoBehaviour {

    public float delay = 3.0f;
    bool hasExploded = false;
    float countdown;

    public float blastRadius;
    public float explosionForce;

	// Use this for initialization
	void Start () {
        countdown = delay;
	}
	
	// Update is called once per frame
	void Update () {
        countdown -= Time.deltaTime;
        if (countdown <= 0.0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
	}

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach(Collider col in colliders)
        {
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if(rb)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }
        }

        Destroy(gameObject);
    }
}
