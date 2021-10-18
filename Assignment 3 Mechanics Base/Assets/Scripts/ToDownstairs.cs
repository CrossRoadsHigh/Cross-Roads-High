using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDownstairs : MonoBehaviour
{
    public GameObject player;
    public GameObject teleportPos;

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
        }
    }
}
