﻿using UnityEngine.Audio;
using System;
using UnityEngine;



public class AudioManager : MonoBehaviour
{
    //List for sounds to be stored in
    public Sound[] sounds;

    //Instance of audio manager
    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {

        //Checks that there is only one instance of audio manager
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
        }
    }

    void Start()
    {
        //Debug.Log("tässäkin voi playaa soundin esim.");
    }
    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!"); 
            return;
        }
        s.source.Play();
    }
}
