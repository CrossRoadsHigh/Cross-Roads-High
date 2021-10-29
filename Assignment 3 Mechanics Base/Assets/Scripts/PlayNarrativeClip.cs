using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayNarrativeClip : MonoBehaviour
{
    public AudioSource thisSource;
    public List<AudioSource> audioSources;
    public bool startedPlaying;

    public void Update()
    {
        if (!thisSource.isPlaying && startedPlaying)
            Destroy(this.gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !thisSource.isPlaying)
        {
            startedPlaying = true;
            thisSource.Play();

            for (int i = 0; i < audioSources.Count; i++)
            {
                if (audioSources[i] != null && audioSources[i].isPlaying)
                {
                    audioSources[i].Stop();
                    Destroy(audioSources[i].gameObject);
                }
            }
        }
    }
}
