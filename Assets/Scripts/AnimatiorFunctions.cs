using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatiorFunctions : MonoBehaviour
{
    [SerializeField] private AudioClip enemyDie;
    
    public GameObject audioManager;
    public GameObject enemy;


    private void Start()
    {
        //audioManager.GetComponent<AudioManager>().sfxAudioSource.PlayOneShot(enemyDie);
    }

    void PlaySound()
    {
        
    }
}
