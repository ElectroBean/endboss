using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    [Header("Health")]
    public float totalHealth;
    public float currentHealth;
    float healthPercent;
    public Slider healthUI;
     

    [Header("NavMesh")]
    public NavMeshAgent NMA;

    [Header("Target")]
    public GameObject target;

    [Header("Phase 1: Nade Phase")]
    public GameObject NadePrefab;
    public float throwDelay = 2.0f;
    public float throwCountdown;
    public float throwForce = 40.0f;

    [Header("Phase 2: Rock Fall")]
    public Transform[] spawnMaxMin;
    public float spawnDelay;
    public float spawnCounter;
    public GameObject rockPrefab;
    public float rockspawnVelocity;
    public float warningTime;
    float warningCountdown;
    public LineRenderer lr;
    bool chosenPos = false;
    Vector3 randomSpawnPoint = new Vector3();

    [Header("Phase 3: Blockable 1 hit")]
    public LineRenderer laserLR;
    public GameObject InitialObject;
    public float shootForce;
    float blockableCDTimer;
    public float blockableCDMax;
    public float lrWarningTime;
    float lrWarningCounter;


    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        throwCountdown = throwDelay;
        warningCountdown = warningTime;
        blockableCDTimer = blockableCDMax;
        lrWarningCounter = lrWarningTime;
        healthPercent = (currentHealth / totalHealth);
        healthUI.value = healthPercent;

        FLoatingTextController.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        healthUI.value = healthPercent = (currentHealth / totalHealth);
        transform.LookAt(target.transform);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
      

        if (healthPercent > 75)
        {
            Phase1();
        }
        else if (healthPercent > 50)
        {
            Phase2();
        }
        else if (healthPercent > 25)
        {
            Phase3();
        }
        else
        {
            Phase4();
        }
    }

    void Phase1()
    {
        throwCountdown -= Time.deltaTime;
        if (throwCountdown <= 0)
        {
            ThrowNade();
            throwCountdown = throwDelay;
        }
    }
    void Phase2()
    {
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            warningCountdown -= Time.deltaTime;

            if (!chosenPos)
            {
                float randomX = Random.Range(spawnMaxMin[0].position.x, spawnMaxMin[1].position.x);
                float Y = 5;
                float randomZ = Random.Range(spawnMaxMin[0].position.y, spawnMaxMin[1].position.y);
                randomSpawnPoint = new Vector3(randomX, Y, randomZ);
                lr.SetPosition(0, randomSpawnPoint);
                lr.SetPosition(1, randomSpawnPoint + -Vector3.up * 5);
                chosenPos = true;

            }

            if (warningCountdown <= 0)
            {
                Instantiate(rockPrefab, randomSpawnPoint, Quaternion.identity);
                spawnCounter = spawnDelay;
                warningCountdown = warningTime;
                chosenPos = false;
            }
        }

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
        FLoatingTextController.CreateFloatingText(damage.ToString(), transform);
    }

    public void BossChargeAttack()
    {
        blockableCDTimer -= Time.deltaTime;
        if (blockableCDTimer <= 0)
        {
            laserLR.SetPosition(0, transform.position);
            laserLR.SetPosition(1, target.transform.position);
            lrWarningCounter -= Time.deltaTime;
            if (lrWarningCounter <= 0)
            {
                laserLR.enabled = false;
                //do the attack

                lrWarningCounter = lrWarningTime;
                blockableCDTimer = blockableCDMax;
            }
 
        }
    }
}
