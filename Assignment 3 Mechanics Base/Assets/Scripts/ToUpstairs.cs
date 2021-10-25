using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToUpstairs : MonoBehaviour
{
    public GameObject player;
    public GameObject teleportPos;
    public GameObject camera;

    public bool teleport;
    public float teleportTimer;
    public float teleportTime;

    // Start is called before the first frame update
    void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > teleportTimer && teleport == false)
        {
            teleport = true;
            teleportTimer = Time.time + teleportTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (teleport == true)
            {
                collision.transform.position = teleportPos.transform.position;
                camera.transform.position = teleportPos.transform.position;
                teleport = false;
            }
        }
    }
}
