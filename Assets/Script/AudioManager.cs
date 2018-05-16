﻿using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public AudioClip InputClick;
    public AudioClip InputHover;
    public AudioClip Ambient;
    public AudioClip Altimeter;
    public AudioClip Alarm;

    public float FadeTime = .5f;
    private List<AudioSource> audioSources = new List<AudioSource>();
    #region API
    /// <summary>
    /// Play a specific AudioType.
    /// Throws error if no Clip is assigned to the specific type.
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_loop"></param>
    public void PlaySound(AudioType _type, bool _loop = false) 
    {
        AudioSource sourceToUse = GetFirstAvaibleSource();

        switch (_type)
        {
            case AudioType.InputClick:
                sourceToUse.clip = InputClick;
                break;
            case AudioType.InputHover:
                sourceToUse.clip = InputHover;
                break;
            case AudioType.Ambient:
                sourceToUse.clip = Ambient;
                break;
            case AudioType.Altimeter:
                sourceToUse.clip = Altimeter;
                break;
            case AudioType.Alarm:
                sourceToUse.clip = Alarm;
                break;

            default:
                break;
        }

        //sourceToUse.volume = 0;
        sourceToUse.Play();
        //sourceToUse.DOFade(1, FadeTime);
        if (_loop)
            sourceToUse.loop = true;
    }
    /// <summary>
    /// Play a specific AudioType.
    /// Throws error if no Clip is assigned to the specific type.
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_loop"></param>
    public void PlaySound(AudioType _type, bool _overrideCurrent, bool _loop = false)
    {
        AudioSource sourceToUse = GetFirstSourceByType(_type);

        switch (_type)
        {
            case AudioType.InputClick:
                sourceToUse.clip = InputClick;
                break;
            case AudioType.InputHover:
                sourceToUse.clip = InputHover;
                break;
            case AudioType.Ambient:
                sourceToUse.clip = Ambient;
                break;
            case AudioType.Altimeter:
                sourceToUse.clip = Altimeter;
                break;
            case AudioType.Alarm:
                sourceToUse.clip = Alarm;
                break;

            default:
                break;
        }

        //sourceToUse.volume = 0;
        sourceToUse.Play();
        //sourceToUse.DOFade(1, FadeTime);
        if (_loop)
            sourceToUse.loop = true;
    }
    /// <summary>
    /// Play a specific Clip.
    /// </summary>
    /// <param name="_type"></param>
    /// <param name="_loop"></param>
    public void PlaySound(AudioClip _clip, bool _loop = false)
    {
        AudioSource sourceToUse = GetFirstAvaibleSource();

        sourceToUse.clip = _clip;

        //sourceToUse.volume = 0;
        sourceToUse.Play();
        //sourceToUse.DOFade(1, FadeTime);
        if (_loop)
            sourceToUse.loop = true;
    }
    #endregion

    /// <summary>
    /// Return the first Avaiable AudioSource.
    /// It creates a new one if none foun.
    /// </summary>
    /// <returns></returns>
    AudioSource GetFirstAvaibleSource()
    {
        AudioSource firstFreeSource = null;
        //Search for thee first avaiable free AudioSource
        foreach (var audio in audioSources)
        {
            if (audio.isPlaying == false)
            {
                firstFreeSource = audio;
                break;
            }
        }
        //If none found it creates a new one;
        if (firstFreeSource == null)
        {
            firstFreeSource = Instantiate(new GameObject("Source_" + audioSources.Count), transform).AddComponent<AudioSource>();
            audioSources.Add(firstFreeSource);
        }

        return firstFreeSource;
    }
    /// <summary>
    /// Return the first AudioSource that's playing a specific AudioType
    /// It creates a new one if none found.
    /// </summary>
    /// <param name="_type"></param>
    /// <returns></returns>
    AudioSource GetFirstSourceByType(AudioType _type)
    {
        AudioSource sourceByType = null;

        AudioClip referenceClip = null;
        switch (_type)
        {
            case AudioType.InputClick:
                referenceClip = InputClick;
                break;
            case AudioType.InputHover:
                referenceClip = InputHover;
                break;
            case AudioType.Ambient:
                referenceClip = Ambient;
                break;
            case AudioType.Altimeter:
                referenceClip = Altimeter;
                break;
            case AudioType.Alarm:
                referenceClip = Alarm;
                break;
            default:
                break;
        }

        foreach (AudioSource source in audioSources)
        {
            if (source.clip = referenceClip)
            {
                sourceByType = source;
                break;
            }
        }

        if(sourceByType == null)
            sourceByType = GetFirstAvaibleSource();


        return sourceByType;
    }
}

public enum AudioType 
{
    InputClick = 0,
    InputHover,
    Ambient,
    Altimeter,
    Alarm
}

