using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : Pickup {

    public int fuel = 15;
    public GameObject audioObject;
    public AudioClip audioClip;

    public override void OnTriggerEnter(Collider other) {

        if (other.tag == "Player") {
            other.transform.GetComponent<PlayerAvatar>().fuel += fuel;

            if (other.transform.GetComponent<PlayerAvatar>().fuel > 50)
                other.transform.GetComponent<PlayerAvatar>().fuel = 50;

            GameObject thisObject = Instantiate(audioObject, transform.position, Quaternion.identity);
            thisObject.GetComponent<AudioSource>().clip = audioClip;

            Destroy(this.gameObject);
        }
    }
}
