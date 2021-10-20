using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToUpstairs : MonoBehaviour
{
    public GameObject player;
    public GameObject teleportPos;
    public GameObject camera;

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
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.position = teleportPos.transform.position;
            camera.transform.position = teleportPos.transform.position;
        }
    }
}
