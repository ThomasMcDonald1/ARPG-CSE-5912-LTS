using UnityEngine.Audio;
using System.Collections;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    void Update()
    {
        Debug.Log((Array.Find(sounds, sound => sound.name == "Footsteps").source == null) + "asdfailsudhf");
    }
    void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        AdjustMusicVolume(PlayerPrefs.GetFloat("BGM", 0.1f));
        AdjustSoundEffectVolume(PlayerPrefs.GetFloat("SE", 1f));
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
        Debug.Log((s.source == null) + "asdcabxcvcbcbb");
        s.StartSound();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.Log("Sound: " + name + " not found");
            return;
        }
        s.StopSound();
    }

    public void AdjustMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("BGM", volume);

        Sound s1 = Array.Find(sounds, sound => sound.name == "Theme");
        Sound s2 = Array.Find(sounds, sound => sound.name == "Dungeon1BGM");
        Sound s3 = Array.Find(sounds, sound => sound.name == "Dungeon2BGM");
        Sound s4 = Array.Find(sounds, sound => sound.name == "Dungeon3BGM");
        Sound s5 = Array.Find(sounds, sound => sound.name == "Boss1Music");
        Sound s6 = Array.Find(sounds, sound => sound.name == "Boss2Music");
        Sound s7 = Array.Find(sounds, sound => sound.name == "Boss3Music");

        if (s1 != null)
        {
            s1.UpdateVolume(volume);
        }
        if (s2 != null)
        {
            s2.UpdateVolume(volume);
        }
        if (s3 != null)
        {
            s3.UpdateVolume(volume);
        }
        if (s4 != null)
        {
            s4.UpdateVolume(volume);
        }
        if (s5 != null)
        {
            s5.UpdateVolume(volume);
        }
        if (s6 != null)
        {
            s6.UpdateVolume(volume);
        }
        if (s7 != null)
        {
            s7.UpdateVolume(volume);
        }
    }

    public void AdjustSoundEffectVolume(float volume)
    {
        PlayerPrefs.SetFloat("SE", volume);

        Sound s1 = Array.Find(sounds, sound => sound.name == "Footsteps");
        Sound s2 = Array.Find(sounds, sound => sound.name == "MenuClick");
        Sound s3 = Array.Find(sounds, sound => sound.name == "Force");

        if (s1 != null)
        {
            s1.UpdateVolume(volume);
        }
        if (s2 != null)
        {
            s2.UpdateVolume(volume);
        }
        if (s3 != null)
        {
            s3.UpdateVolume(volume);
        }

    }

}
