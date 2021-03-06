using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnPlayerDamage : MonoBehaviour
{
    public float damage = 10;

    public List<Transform> burnTargets = new List<Transform>();

    private float damageTime = 0.25f;
    private float damageTimer;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.parent.tag == "Player")
            burnTargets.Add(transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > damageTimer)
        {
            foreach (Transform target in burnTargets)
            {
                if (target != null)
                    target.GetComponent<PlayerAvatar>().takeDamage(damage);
            }
            damageTimer = Time.time + damageTime;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!burnTargets.Contains(other.gameObject.transform))
                burnTargets.Add(other.gameObject.transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            burnTargets.Remove(other.gameObject.transform);
        }
    }
}
