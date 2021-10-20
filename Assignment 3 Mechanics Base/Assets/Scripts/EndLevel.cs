using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public GameObject tester;
    public bool gameEnd;


    // Start is called before the first frame update
    void Start()
    {
        gameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("Enemy-Boss"))
        {
            gameEnd = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" && gameEnd)
        {
            Debug.Log("Worked");
            Application.Quit();
        }
    }
}
