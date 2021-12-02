using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatiorFunctions : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    
    public GameObject audioManager;
    public GameObject enemy;


    private void Start()
    {
        audioManager.GetComponent<AudioManager>().PlayOneShot();
    }

    void PlaySound()
    {
        
    }
}
