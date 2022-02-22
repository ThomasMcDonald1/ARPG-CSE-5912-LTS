using UnityEngine.Audio;
using System.Collections;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;


    public static AudioManager instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
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
        }

    }

    void Start()
    {
        AdjustMusicVolume(PlayerPrefs.GetFloat("BGM", 0.1f));
        AdjustSoundEffectVolume(PlayerPrefs.GetFloat("SE", 1f));
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("Sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("Sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }

    public void AdjustMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("BGM", volume);

        Sound s = Array.Find(sounds, sound => sound.name == "Theme");
        if (s != null)
        {
            s.volume = volume;
            s.source.volume = volume;
        }
    }

    public void AdjustSoundEffectVolume(float volume)
    {
        PlayerPrefs.SetFloat("SE", volume);

        Sound s1 = Array.Find(sounds, sound => sound.name == "Footsteps");
        Sound s2 = Array.Find(sounds, sound => sound.name == "MenuClick");
        if (s1 != null)
        {
            s1.volume = volume;
            s1.source.volume = volume;
        }
        if (s2 != null)
        {
            s2.volume = volume;
            s2.source.volume = volume;
        }

    }

}
