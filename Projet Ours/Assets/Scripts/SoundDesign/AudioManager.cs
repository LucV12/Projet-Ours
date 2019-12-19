using UnityEngine.Audio;
using System;
using UnityEngine;
using Random = UnityEngine.Random;


/*pour ajouer un son quelque part ajputer cette ligne à l'endroit voulu :
FindObjectOfType<AudioManager>().Play("name");*/
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] pas;
    public Sound[] music;
    float currentTime;

    int index;
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.pitch = 1;
            s.volume = 0.5f;
        }
        foreach (Sound s in pas)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.pitch = 1;
            s.volume = 0.5f;
        }
        foreach (Sound s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.pitch = 1;
            s.volume = 0.5f;
        }

    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    /*void Start()
    {
        PlayMusic();
    }
    void Update()
    {
        currentTime += Time.deltaTime;
    }
    public void Course()
    {
       Sound s = pas[Mathf.RoundToInt(Random.value * (pas.Length - 1))];
       s.source.Play();
    }
    public void PlayMusic()
    {
        Sound s = music[Mathf.RoundToInt(Random.value * (music.Length - 1))];
        s.source.Play();
        if (currentTime > s.clip.length)
        {
            PlayMusic();
            currentTime = 0;
        }
    }*/
}
