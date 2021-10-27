using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public float speed = 1;

    public float y = -75;

    void Update()
    {
        y += speed * Time.deltaTime;

        if (y > -20)
        {
            speed *= -1;
        }
        else if (y < -75)
        {
            speed *= -1;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z);
    }
}
