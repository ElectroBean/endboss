using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour {

    [Header("Health")]
    public float totalHealth;
    public float currentHealth;
    float healthPercent;

    [Header("NavMesh")]
    public NavMeshAgent NMA;

    [Header("Target")]
    public GameObject target;

    [Header("Phase 1: Nade Phase")]
    public GameObject NadePrefab;
    public float throwDelay = 2.0f;
    public float throwCountdown;
    public float throwForce = 40.0f;

	// Use this for initialization
	void Start () {
        throwCountdown = throwDelay;
	}
	
	// Update is called once per frame
	void Update () {
        healthPercent = (currentHealth / totalHealth) * 100;
        transform.LookAt(target.transform);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
        
        if(healthPercent > 75)
        {
            Phase1();
        }
        else if(healthPercent > 50)
        {

        }
        else if(healthPercent > 25)
        {

        }
        else
        {

        }
	}

    void Phase1()
    {
        throwCountdown -= Time.deltaTime;
        if(throwCountdown <= 0)
        {
            ThrowNade();
            throwCountdown = throwDelay;
        }
    }
    void Phase2()
    {

    }
    void Phase3()
    {

    }
    void Phase4()
    {

    }

    void ThrowNade()
    {
        GameObject grenade = Instantiate(NadePrefab, transform.position, transform.rotation);
        Rigidbody nadeRB = grenade.GetComponent<Rigidbody>();
        nadeRB.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
