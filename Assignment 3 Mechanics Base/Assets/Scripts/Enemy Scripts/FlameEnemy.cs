using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlameEnemy : MonoBehaviour
{
    NavMeshAgent agent;
    private Animator anim;

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
    private float laserTime = 1.0f;

    public GameObject flameStream;
    private float flamerTimer;
    private float flamerTime = 10.0f;

    //Collision Damage
    private float damageTimer;
    private float damageTime = 0.5f;

    public GameObject explosion;

    public GameObject enemyFireDamage;
    private float FTFireTime = 0.2f;
    private float FTFireTimer;

    public float dropChance;
    public GameObject HealthPack;
    public GameObject AmmoBox;
    public GameObject FuelPack;

    //Audio Variables
    public GameObject audioObjectPrefab;
    public AudioSource source;
    public AudioClip deathClip;
    public AudioClip detectClip;
    public AudioClip flameClip;
    public bool wasDetected;
    public bool wasShooting;

    // Use this for initialization
    void Start()
    {
        // Minions spawn facing a random direction
        if (this.name == "Flame-Enemy-Boss")
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
        

        //fireBool = false;
        agent = GetComponent<NavMeshAgent>();
        flameStream.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (flameStream.GetComponent<ParticleSystem>().isPlaying)
        {
            if (!wasShooting)
            {
                wasShooting = true;
                source.clip = flameClip;
                source.Play();
            }
        }
        else
        {
            source.Stop();
            wasShooting = false;
        }

        Behaviour();

        //Kill check - moved from takeDamage due to bug
        if (health <= 0)
        {
            flameStream.GetComponent<ParticleSystem>().Stop();
            Instantiate(explosion, transform.position, transform.rotation);
            GameObject thisAudioObject = Instantiate(audioObjectPrefab, transform.position, Quaternion.identity);
            thisAudioObject.GetComponent<AudioSource>().clip = deathClip;

            if (dropChance > Random.Range(1, 100))
            {
                Vector3 dropPoint = transform.position;
                dropPoint = new Vector3(dropPoint.x, dropPoint.y - 1.5f, dropPoint.z);

                float packDrop = Random.Range(1, 100);
                Debug.Log(packDrop);

                if (33.0f > packDrop)
                {
                    Instantiate(AmmoBox, dropPoint, transform.rotation);
                }
                else if (66.0f > packDrop)
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

                    //Fire flamer
                    if (Time.time > flamerTimer)
                    {
                        flameStream.GetComponent<ParticleSystem>().Play();
                        flamerTimer = Time.time + flamerTime;
                    }

                    if (Time.time > FTFireTimer)
                    {
                        Instantiate(enemyFireDamage, flameStream.transform.position, transform.rotation);
                        FTFireTimer = Time.time + FTFireTime;
                    }
                }
            }
            else
                wasDetected = false;
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
