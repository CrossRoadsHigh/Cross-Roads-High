using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioClip : MonoBehaviour
{
    public AudioSource thisSource;

    public void Start()
    {
        thisSource.Play();
    }

    public void Update()
    {
        if (!thisSource.isPlaying)
            Destroy(this.gameObject);
    }
}
