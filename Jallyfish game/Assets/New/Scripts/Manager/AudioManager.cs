using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    //смысл в том чтобв иметь список звков которые мы можем добавлять или удалять по ходу работы
    public Sound[] sounds;//массив с музыкой

    private Dictionary<string, Sound> Sounds = new Dictionary<string, Sound>(); //словарь с музлом и именем

    private void Awake()
    {
        foreach (Sound s in sounds) //проходимся внутри массива с музлом
        {
            if (s == null) continue; //пропуск если такой песни нет

            s.source = gameObject.AddComponent<AudioSource>(); //добавляем на этот компонет музло
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            Sounds.Add(s.name, s);
        }
    }

    public void Play(string name) //метод проигрывает музыку
    {
        if (Sounds.ContainsKey(name)) //если песня соответствует имени то музло играет
        {
            Sound s = Sounds[name];
            s.source.Play();
        }
        else //иначе ошибка
        {
            Debug.Log("Sound not found: " + name);
        }
    }
}