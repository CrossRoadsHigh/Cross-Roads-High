using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Pickup {

    public int ammo = 50;
    public GameObject audioObject;
    public AudioClip audioClip;

    public override void OnTriggerEnter(Collider other) {

        if (other.tag == "Player") {
            other.transform.GetComponent<PlayerAvatar>().ammo += ammo;

            if (other.transform.GetComponent<PlayerAvatar>().ammo > 500)
                other.transform.GetComponent<PlayerAvatar>().ammo = 500;

            GameObject thisObject = Instantiate(audioObject, transform.position, Quaternion.identity);
            thisObject.GetComponent<AudioSource>().clip = audioClip;

            Destroy(this.gameObject);
        }
    }
}
