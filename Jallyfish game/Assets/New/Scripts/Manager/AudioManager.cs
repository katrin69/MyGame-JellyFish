using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    //смысл в том чтобв иметь список звков которые мы можем добавлять или удалять по ходу работы
    public Sound[] sounds;

    private Dictionary<string, Sound> Sounds = new Dictionary<string, Sound>();

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            if (s == null) continue; 

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            Sounds.Add(s.name, s);
        }
    }

    public void Play(string name)
    {
        if (Sounds.ContainsKey(name))
        {
            Sound s = Sounds[name];
            s.source.Play();
        }
        else
        {
            Debug.Log("Sound not found: " + name);
        }
    }
}