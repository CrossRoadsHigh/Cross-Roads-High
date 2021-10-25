using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public GameObject tester;
    public bool gameEnd;
    public int level;


    // Start is called before the first frame update
    void Start()
    {
        gameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (level == 1)
        {
            if (!GameObject.Find("Enemy-Boss"))
            {
                gameEnd = true;
            }
        }
        else if (level == 2)
        {
            if (!GameObject.Find("Flame-Enemy-Boss"))
            {
                gameEnd = true;
            }
        }
        else if (level == 3)
        {
            if (!GameObject.Find("Spread-Enemy-Boss"))
            {
                gameEnd = true;
            }
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" && gameEnd)
        {
            if (level == 1)
            {
                Application.LoadLevel("Level 2 - New Models");
            }
            else if (level == 2)
            {
                Application.LoadLevel("Level 3 - New Models"); 
            }
            else if (level == 3)
            {
                Application.Quit();
            }
            
        }
    }
}
