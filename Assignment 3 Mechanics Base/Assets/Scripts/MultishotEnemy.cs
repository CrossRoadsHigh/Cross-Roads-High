using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MultishotEnemy : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject player;
    public GameObject minion;
    public bool boss;
    public float spawnChance;

    public float health = 10.0f;

    public float agroRange = 10.0f;
    public float damage = 5.0f;

    //Rotation vars
    public float rotationSpeed;
    private float adjRotSpeed;
    public Quaternion targetRotation;

    //Laser Damage
    public GameObject laser;
    public GameObject laserMuzzle;
    public float laserAngle;

    private float laserTimer;
    public float laserTime;

    //Collision Damage
    private float damageTimer;
    private float damageTime = 0.5f;

    public GameObject burning;
    public GameObject explosion;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (this.gameObject.name == "Spread-Enemy-Boss")
        {
            boss = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Behaviour();

        //Kill check - moved from takeDamage due to bug
        if (health <= 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    void Behaviour()
    {

        if (!player)
            player = GameObject.FindGameObjectWithTag("Player");
        else if (player && !GameManager.instance.playerDead)
        {

            //Raycast in direction of Player
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -(transform.position - player.transform.position).normalized, out hit, agroRange))
            {

                //If Raycast hits player
                if (hit.transform.tag == "Player")
                {

                    Debug.DrawLine(transform.position, player.transform.position, Color.red);

                    //Rotate slowly towards player
                    targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
                    adjRotSpeed = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, adjRotSpeed);

                    //Move towards player
                    if (Vector3.Distance(player.transform.position, transform.position) >= 5)
                    {
                        agent.SetDestination(player.transform.position);
                    }
                    //Stop if close to player
                    else if (Vector3.Distance(player.transform.position, transform.position) < 5)
                    {
                        agent.SetDestination(transform.position);
                    }

                    //Fire Laser
                    if (Time.time > laserTimer)
                    {
                        Vector3 rightShot = laserMuzzle.transform.rotation.eulerAngles;
                        rightShot = new Vector3(rightShot.x, rightShot.y + laserAngle, rightShot.z);

                        Vector3 leftShot = laserMuzzle.transform.rotation.eulerAngles;
                        leftShot = new Vector3(leftShot.x, leftShot.y - laserAngle, leftShot.z);

                        Instantiate(laser, laserMuzzle.transform.position, laserMuzzle.transform.rotation);
                        Instantiate(laser, laserMuzzle.transform.position, Quaternion.Euler(rightShot));
                        Instantiate(laser, laserMuzzle.transform.position, Quaternion.Euler(leftShot));

                        //Spawn minions
                        if (boss && spawnChance > Random.Range(1, 100))
                        {
                            Vector3 rightSpawn = laserMuzzle.transform.position;
                            rightSpawn = new Vector3(rightSpawn.x + 0.7f, rightSpawn.y, rightSpawn.z + 1.2f);

                            Vector3 leftSpawn = laserMuzzle.transform.position;
                            leftSpawn = new Vector3(leftSpawn.x - 0.7f, leftSpawn.y, leftSpawn.z + 1.2f);

                            Instantiate(minion, rightSpawn, laserMuzzle.transform.rotation);
                            Instantiate(minion, leftSpawn, laserMuzzle.transform.rotation);
                            //Instantiate(minion, laserMuzzle.transform.position, laserMuzzle.transform.rotation);
                        }



                        laserTimer = Time.time + laserTime;
                    }
                }
            }
        }
    }


    private void OnCollisionStay(Collision collision)
    {

        if (collision.transform.tag == "Player" && Time.time > damageTimer)
        {
            collision.transform.GetComponent<PlayerAvatar>().takeDamage(damage);
            damageTimer = Time.time + damageTime;
        }
    }

    public void takeDamage(float thisDamage)
    {

        health -= thisDamage;
    }
}
