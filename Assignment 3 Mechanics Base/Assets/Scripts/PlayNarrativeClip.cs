using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayNarrativeClip : MonoBehaviour
{
    public AudioSource thisSource;
    public AudioSource musicSource;
    public List<AudioSource> audioSources;
    public bool startedPlaying;
    public string speakerName;

    public GameObject narrativeGUI;
    public Text speakerNameText;

    public void Start()
    {
        musicSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (!thisSource.isPlaying && startedPlaying)
        {
            musicSource.volume = 0.8f;
            narrativeGUI.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !thisSource.isPlaying)
        {
            startedPlaying = true;
            thisSource.Play();
            musicSource.volume = 0.4f;
            speakerNameText.text = speakerName;
            narrativeGUI.SetActive(true);

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
