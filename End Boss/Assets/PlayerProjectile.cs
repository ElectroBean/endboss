using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {

    public float damage;
    public float deadTime;
    float deadTimeCounter;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boss")
        {
            Boss b = collision.gameObject.GetComponent<Boss>();
            b.TakeDamage(damage);
        }
    }
}
