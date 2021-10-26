using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile {

    public GameObject audioObjectPrefab;
    public AudioClip enemyHitClip;

    public override void OnTriggerEnter(Collider otherObject) {

        if (otherObject.tag == "Enemy") 
        { 
            if (otherObject.GetComponent<Enemy>())
            {
                otherObject.GetComponent<Enemy>().takeDamage(damage);
                Instantiate(hitEffect, transform.position, transform.rotation);

                GameObject thisAudioObject = Instantiate(audioObjectPrefab, transform.position, Quaternion.identity);
                thisAudioObject.GetComponent<AudioSource>().clip = enemyHitClip;

                Destroy(this.gameObject);
            }
            else if (otherObject.GetComponent<FlameEnemy>())
            {
                otherObject.GetComponent<FlameEnemy>().takeDamage(damage);
                Instantiate(hitEffect, transform.position, transform.rotation);

                GameObject thisAudioObject = Instantiate(audioObjectPrefab, transform.position, Quaternion.identity);
                thisAudioObject.GetComponent<AudioSource>().clip = enemyHitClip;

                Destroy(this.gameObject);
            }
            else if (otherObject.GetComponent<MultishotEnemy>())
            {
                otherObject.GetComponent<MultishotEnemy>().takeDamage(damage);
                Instantiate(hitEffect, transform.position, transform.rotation);

                GameObject thisAudioObject = Instantiate(audioObjectPrefab, transform.position, Quaternion.identity);
                thisAudioObject.GetComponent<AudioSource>().clip = enemyHitClip;

                Destroy(this.gameObject);
            }
        }
        else if (otherObject.tag == "Environment") {
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
