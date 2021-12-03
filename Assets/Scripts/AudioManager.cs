using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioManager : MonoBehaviour
{
    public AudioSource sfxAudioSource;
    public AudioSource musicAudioSource;
    public AudioSource ambienceAudioSource;

    //Singleton Instantiation - Now you can reference the AudioManager
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<AudioManager>();
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
