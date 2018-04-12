using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    
    public AudioData ambient;
    public AudioData beep;
    public AudioData allarm;
    private List<AudioSource> audioSources = new List<AudioSource>();

    // Use this for initialization
    void Start () {
        foreach (AudioSource source in GameObject.FindObjectsOfType<AudioSource>()) {
            audioSources.Add(source);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.P)) {
            PlaySound(AudioType.ambient);
        }


    }

    public void PlaySound(AudioType audioTypes) 
    {
        AudioSource current = null;
        foreach (var audio in audioSources) 
        {
            
            if(audio.isPlaying == false) {
                current = audio;
                break;
            }
        }

        switch (audioTypes) {
            case AudioType.ambient:
                current.clip = ambient.Clip;
                current.Play();
                break;
            case AudioType.beep:
                current.clip = beep.Clip;
                current.Play();
                break;
            case AudioType.allarme:
                current.clip = allarm.Clip;
                current.Play();
                break;
            default:
                break;
        }

        
    }
    

}
public enum AudioType 
{
    ambient = 1,
    beep = 2,
    allarme = 3,
}

[Serializable]
public class AudioData 
{
    public AudioClip Clip;

}

