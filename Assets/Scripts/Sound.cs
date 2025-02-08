using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float rawVolume;
    [Range(0f, 2f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;
}
