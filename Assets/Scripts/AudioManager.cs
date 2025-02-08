using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sfxSounds;
    public Sound[] soundtracks;
    private float musicVolume;
    private float sfxVolume;
    public static AudioManager Instance;
    public void Awake() {
        if (Instance  == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sfxSounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.rawVolume;
        }

        musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.75f);
        sfxVolume = PlayerPrefs.GetFloat("sfxVolue", 0.75f);
    }

    public void Play(int index) {
        sfxSounds[index].source.Play();
    }
    public void Play(string name) {
        Sound s = Array.Find(sfxSounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound " + name + " could not be found.");
            return;
        }

        s.source.Play();
    }
    public float GetMusicVolume() {
        return musicVolume;
    }
    public float GetSFXVolume() {
        return sfxVolume;
    }
    public void SetMusicVolume(float volume) {
        musicVolume = volume;
    }
    public void SetSFXVolume(float volume) {
        sfxVolume = volume;
    }
}
