using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    NavMeshAgent agent;

    public GameObject player;

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
    private float laserTimer;
    public float laserTime;

    //Collision Damage
    private float damageTimer;
    private float damageTime = 0.5f;

    public GameObject burning;
    public GameObject explosion;

    //Audio Variables
    public GameObject audioObjectPrefab;
    public AudioClip deathClip;
    public AudioClip detectClip;
    public bool wasDetected;

    public float dropChance;
    public GameObject HealthPack;
    public GameObject AmmoBox;
    public GameObject FuelPack;

    // Use this for initialization
    void Start () {

        // Minions spawn facing a random direction
        if (this.name == "Enemy-Boss")
        {

        }
        else
        {
            float ranY = Random.Range(0.0f, 360.0f);
            Vector3 ranYVector;
            ranYVector.x = this.transform.rotation.x;
            ranYVector.y = ranY;
            ranYVector.z = this.transform.rotation.z;
            this.transform.rotation = Quaternion.Euler(ranYVector);
        }

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {

        Behaviour();

        //Kill check - moved from takeDamage due to bug
        if (health <= 0) {
            Instantiate(explosion, transform.position, transform.rotation);
            GameObject thisAudioObject = Instantiate(audioObjectPrefab, transform.position, Quaternion.identity);
            thisAudioObject.GetComponent<AudioSource>().clip = deathClip;

            if (dropChance > Random.Range(1, 100))
            {
                Vector3 dropPoint = transform.position;
                dropPoint = new Vector3(dropPoint.x, dropPoint.y - 1.5f, dropPoint.z);

                float packDrop = Random.Range(1, 100);
                Debug.Log(packDrop);

                if (25.0f > packDrop)
                {
                    Instantiate(AmmoBox, dropPoint, transform.rotation);
                }
                else if (50.0f > packDrop)
                {
                    Instantiate(FuelPack, dropPoint, transform.rotation);
                }
                else
                {
                    Instantiate(HealthPack, dropPoint, transform.rotation);
                }
            }

            Destroy(this.gameObject);
        }
    }

    void Behaviour() {

        if (!player)
            player = GameObject.FindGameObjectWithTag("Player");
        else if (player && !GameManager.instance.playerDead) {

            //Raycast in direction of Player
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -(transform.position - player.transform.position).normalized, out hit, agroRange))
            {

                //If Raycast hits player
                if (hit.transform.tag == "Player")
                {

                    if (!wasDetected)
                    {
                        GameObject thisAudioObject = Instantiate(audioObjectPrefab, transform.position, Quaternion.identity);
                        thisAudioObject.GetComponent<AudioSource>().clip = detectClip;
                    }
                    wasDetected = true;

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
                        Instantiate(laser, laserMuzzle.transform.position, laserMuzzle.transform.rotation);
                        laserTimer = Time.time + laserTime;
                    }
                }
            }
            else
                wasDetected = false;
        }
    }


    private void OnCollisionStay(Collision collision) {

        if (collision.transform.tag == "Player" && Time.time > damageTimer) {
            collision.transform.GetComponent<PlayerAvatar>().takeDamage(damage);
            damageTimer = Time.time + damageTime;
        }
    }

    public void takeDamage(float thisDamage) {

        health -= thisDamage;
    }

}
