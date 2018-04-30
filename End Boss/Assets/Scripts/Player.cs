using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("GameObjects")]
    public GameObject shootingArm;
    public GameObject target;

    [Header("Movement")]
    public float movementSpeed;
    Rigidbody rb;

    [Header("Abilities")]
    public float ChargeUpTime;
    public float chargeTimer = 0.0f;
    public bool hasShot = false;
    public GameObject playerProjectile;
    GameObject tempProjectile;
    public float shootForce; 

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Boss");
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        shootingArm.transform.LookAt(target.transform);
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            tempProjectile = Instantiate(playerProjectile, transform.position + transform.forward * 1.5f, shootingArm.transform.rotation);
        }

        if(Input.GetKey(KeyCode.Alpha1))
        {
            chargeTimer += Time.deltaTime;
            tempProjectile.transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
            Debug.Log("charging");
            if(chargeTimer >= ChargeUpTime && !hasShot)
            {
                //do stuff
                Debug.Log("shot");
                tempProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce, ForceMode.VelocityChange);
                hasShot = true;
            }
        }
        
        if(Input.GetKeyUp(KeyCode.Alpha1))
        {
            chargeTimer = 0.0f;
            hasShot = false;
        }
	}

    private void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementPos = transform.right * horizontal;
        movementPos += transform.forward * vertical;

        rb.MovePosition(transform.position + movementPos * movementSpeed * Time.deltaTime);

    }
}
