using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("GameObjects")]
    public GameObject shootingArm;
    public GameObject target;

    [Header("Movement")]
    private bool canMove = true;
    public float movementSpeed;
    Rigidbody rb;

    [Header("Charge Ability")]
    public float ChargeUpTime;
    public float chargeTimer = 0.0f;
    public bool hasShot = false;
    public GameObject playerProjectile;
    GameObject tempProjectile;
    public float shootForce;

    [Header("Channeled Ability")]
    public float dps;
    public LineRenderer lr;
    float damageCounter = 0.0f;
    float damageDelay = 1.0f;
    public float maxRayDist;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Boss");
        rb = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        shootingArm.transform.LookAt(target.transform);

        /////////////////////////Charged ability/////////////////////////
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            canMove = false;
            tempProjectile = Instantiate(playerProjectile, transform.position + transform.forward * 1.5f, shootingArm.transform.rotation);
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            chargeTimer += Time.deltaTime;
            if (tempProjectile)
                tempProjectile.transform.localScale += new Vector3(0.005f, 0.005f, 0.005f);
            Debug.Log("charging");
            if (chargeTimer >= ChargeUpTime && !hasShot)
            {
                //do stuff
                Debug.Log("shot");
                tempProjectile.GetComponent<Rigidbody>().AddForce(tempProjectile.transform.forward * shootForce, ForceMode.VelocityChange);
                hasShot = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            if(!hasShot)
            {
                Destroy(tempProjectile);
            }
            chargeTimer = 0.0f;
            hasShot = false;
            canMove = true;
        }
        /////////////////////////Charged ability End/////////////////////////

        /////////////////////////Channeled ability///////////////////////////

        if (Input.GetKey(KeyCode.Alpha2))
        {
            canMove = false;
            damageCounter -= Time.deltaTime;
            //enabled linerenderer
            //raycast collision
            Ray newRay = new Ray(shootingArm.transform.position + shootingArm.transform.forward * 0.1f, shootingArm.transform.forward * maxRayDist);
            Debug.DrawRay(shootingArm.transform.position + shootingArm.transform.forward * 0.1f, shootingArm.transform.forward * maxRayDist, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(newRay, out hit))
            {
                //set linerenderer end to collision
                //deal damage
                if (damageCounter <= 0)
                {
                    Boss nb = hit.collider.gameObject.GetComponent<Boss>();
                    if (nb)
                        nb.TakeDamage(dps);
                    damageCounter = damageDelay;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            canMove = true;
        }

        /////////////////////////Channeled ability end///////////////////////
    }

    private void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (canMove)
        {
            Vector3 movementPos = transform.right * horizontal;
            movementPos += transform.forward * vertical;
            rb.MovePosition(transform.position + movementPos * movementSpeed * Time.deltaTime);

        }


    }
}
