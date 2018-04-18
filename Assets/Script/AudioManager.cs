using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public List<AudioData> AudioDatas;
    public int IndipendentChannels = 5;
    private List<AudioSource> audioSources = new List<AudioSource>();

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < IndipendentChannels; i++)
        {
            audioSources.Add(Instantiate(new GameObject("Source_" + i), transform).AddComponent<AudioSource>());
        }
	}

    public void PlaySound(AudioType _type) 
    {
        AudioSource firstFreeSource = null;
        foreach (var audio in audioSources) 
        {
            if(audio.isPlaying == false) {
                firstFreeSource = audio;
                break;
            }
        }

        foreach (AudioData data in AudioDatas)
        {
            if(data.Type == _type)
            {
                firstFreeSource.clip = data.Clip;
                firstFreeSource.Play();
            }
        }
    }
}

public enum AudioType 
{
    Input = 0,
    Ambient = 1,
    Altimeter = 2,
    Allarme = 3,
    Other
}

[Serializable]
public class AudioData 
{
    public AudioType Type;
    public AudioClip Clip;
}

