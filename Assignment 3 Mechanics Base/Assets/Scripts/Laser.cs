using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Projectile {

    public GameObject audioObjectPrefab;
    public AudioClip laserClip;

    public void Awake()
    {
        GameObject thisAudioObject = Instantiate(audioObjectPrefab, transform.position, Quaternion.identity);
        thisAudioObject.GetComponent<AudioSource>().clip = laserClip;
    }

    public override void OnTriggerEnter(Collider otherObject) {

        if (otherObject.tag == "Player") {
            otherObject.GetComponent<PlayerAvatar>().takeDamage(damage);
            //Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        else if (otherObject.tag == "Environment") {
            //Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
