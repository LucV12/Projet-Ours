using UnityEngine.Audio;
using System;
using UnityEngine;
using Random = UnityEngine.Random;


/*pour ajouer un son quelque part ajputer cette ligne à l'endroit voulu :
FindObjectOfType<AudioManager>().Play("name");*/
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
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
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    void Start()
    {
        Play("Musique");
    }

}
