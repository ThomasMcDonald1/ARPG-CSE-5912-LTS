using UnityEngine.Audio;
using System.Collections;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    public string currentBGM;
    public string currentBossMusic;
    private bool fadingMusic;
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
        AdjustSoundEffectVolume(PlayerPrefs.GetFloat("SE", 0.15f));
    }

    void Start()
    {
        AdjustMusicVolume(PlayerPrefs.GetFloat("BGM", 0.1f));
        AdjustSoundEffectVolume(PlayerPrefs.GetFloat("SE", 0.15f));
        Play("TownBGM");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.name == "Footsteps")
        {
            // s.pitch = Math.
        }
        if(s == null)
        {
            Debug.Log("Sound: " + name + " not found");
            return;
        }
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
        Sound s5 = Array.Find(sounds, sound => sound.name == "Boss1BGM");
        Sound s6 = Array.Find(sounds, sound => sound.name == "Boss2BGM");
        Sound s7 = Array.Find(sounds, sound => sound.name == "Boss3BGM");

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
        Sound s1 = Array.Find(sounds, sound => sound.name == "Footsteps");
        Sound s2 = Array.Find(sounds, sound => sound.name == "MenuClick");
        Sound s3 = Array.Find(sounds, sound => sound.name == "Force");
        Sound s4 = Array.Find(sounds, sound => sound.name == "BasicPunch");
        Sound s5 = Array.Find(sounds, sound => sound.name == "Casting");
        Sound s6 = Array.Find(sounds, sound => sound.name == "FireAbility");
        Sound s7 = Array.Find(sounds, sound => sound.name == "ColdAbility");
        Sound s8 = Array.Find(sounds, sound => sound.name == "LightningAbility");
        Sound s9 = Array.Find(sounds, sound => sound.name == "Slash");
        Sound s10 = Array.Find(sounds, sound => sound.name == "PhysicalAbility");
        Sound s11 = Array.Find(sounds, sound => sound.name == "LevelUp");
        Sound s12 = Array.Find(sounds, sound => sound.name == "HealingAbility");
        Sound s13 = Array.Find(sounds, sound => sound.name == "KnockbackAbility");
        Sound s14 = Array.Find(sounds, sound => sound.name == "PullAbility");

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
        if (s8 != null)
        {
            s8.UpdateVolume(volume);
        }
        if (s9 != null)
        {
            s9.UpdateVolume(volume);
        }
        if (s10 != null)
        {
            s10.UpdateVolume(volume);
        }
        if (s11 != null)
        {
            s11.UpdateVolume(volume);
        }
        if (s12 != null)
        {
            s12.UpdateVolume(volume);
        }
        if (s13 != null)
        {
            s13.UpdateVolume(volume);
        }
        if (s14 != null)
        {
            s14.UpdateVolume(volume);
        }

    }
    private IEnumerator FadeOutMusic()
    {
        Sound s = Array.Find(sounds, sound => sound.name == currentBGM);
        float total = s.volume;
        while (s.volume > 0)
        {
            s.volume -= 0.33f * total * Time.deltaTime;
            yield return null;
        }
        s.StopSound();
        Sound s1 = Array.Find(sounds, sound => sound.name == currentBossMusic);
        if (s1 != null)
        {
            s1.StartSound();
        }
        fadingMusic = false;
    }
    public void FadeOut(string music, string nextSong = "")
    {
        // if (currentBossMusic == nextSong) return;
        currentBGM = music;
        currentBossMusic = nextSong;
        if (!fadingMusic)
            fadingMusic = true;
            StartCoroutine(FadeOutMusic());
    }
}
