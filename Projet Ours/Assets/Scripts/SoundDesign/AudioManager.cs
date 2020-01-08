using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


/*pour ajouer un son quelque part ajputer cette ligne à l'endroit voulu :
FindObjectOfType<AudioManager>().Play("name");*/
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    float currentTime;
    public static AudioManager instance;
    bool hubMusique;
    bool runMusique;

    int index;
    void Awake()
    {
        instance = this;
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
    public void PlayThis(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Mute(string name, bool muteState)
    {
        
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //s.source.volume = 0;
        switch(muteState)
        {
            case false:
                s.source.mute = false;
                break;
            case true:
                s.source.mute = true;
                break;
        }
    }

    /*public void OnSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = 1;
    }*/

    private void OnEnable()
    {
        PlayThis("Musique");
        PlayThis("MusiqueHub");
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Hub" && hubMusique == false)
        {
            Debug.Log("Hub");
            MusiqueHub();
            hubMusique = true;
            runMusique = false;
        }
        else if (SceneManager.GetActiveScene().name == "Run" && runMusique == false)
        {
            Debug.Log("");
            MusiqueRun();
            runMusique = true;
            hubMusique = false;
        }
    }

    /*void Start()
    {

        PlayThis("Musique");
        PlayThis("MusiqueHub");

    }*/



    public void MusiqueHub()
    {
        Mute("Musique", true);
        Mute("MusiqueHub", false);
    }

    public void MusiqueRun()
    {
        Mute("Musique", false);
        Mute("MusiqueHub", true);
    }

}
