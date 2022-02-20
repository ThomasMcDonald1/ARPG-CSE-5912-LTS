using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3)]
    public float pitch;
    public bool loop;
    //[HideInInspector]
    public AudioSource source;

    public void StartSound()
    {
        this.source.Play();
    }

    public void StopSound()
    {
        this.source.Stop();
    }

    public void PauseSound()
    {
        this.source.Pause();
    }

    public void UnpauseSound()
    {
        this.source.UnPause();
    }

    public void UpdateVolume(float vol)
    {
        this.volume = vol;
        this.source.volume = vol;
    }

    public void UpdatePitch(float pit)
    {
        this.pitch = pit;
        this.source.pitch = pit;
    }

    public void UpdateLoop(bool lp)
    {
        this.loop = lp;
        this.source.loop = lp;
    }

}
