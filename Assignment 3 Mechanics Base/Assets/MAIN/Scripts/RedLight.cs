using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLight : MonoBehaviour
{
    public float speed = 1;

    public float y = 0;

    void Update()
    {
        y += speed * Time.deltaTime;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z);
    }
}
