using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatiorFunctions : MonoBehaviour
{
    public GameObject audioManager;

    [Header("Player")]
    [SerializeField] private AudioClip playerJump;
    [SerializeField] private float playerJumpVolume = 1f;
    [SerializeField] private AudioClip playerDash;
    [SerializeField] private float playerDashVolume = 1f;
    [SerializeField] private AudioClip playerSwing;
    [SerializeField] private float playerSwingVolume = 1f;
    [SerializeField] private AudioClip playerHurt;
    [SerializeField] private float playerHurtVolume = 1f;

    [Header("Enemy")]
    [SerializeField] private AudioClip [] enemyDie; //Make an array and make it flip between randoms.
    [SerializeField] private float enemyDieVolume = 1f;
    [SerializeField] private AudioClip enemyDie2;
    [SerializeField] private float EnemyDie2Volume = 1f;
    [SerializeField] private AudioClip enemyDie3;
    [SerializeField] private float EnemyDie3Volume = 1f;

    [Header("Pickups")]
    [SerializeField] private AudioClip cottonPickup;
    [SerializeField] private float cottonPickupVolume = 1f;


    //Player Sounds
    void PlayerJump()
    {
        audioManager.GetComponent<AudioManager>().sfxAudioSource.PlayOneShot(playerJump, playerJumpVolume);
    }

    void PlayerDash()
    {
        audioManager.GetComponent<AudioManager>().sfxAudioSource.PlayOneShot(playerDash, playerDashVolume);
    }

    void PlayerSwing()
    {
        audioManager.GetComponent<AudioManager>().sfxAudioSource.PlayOneShot(playerSwing, playerSwingVolume);
    }
    void PlayerHurt()
    {
        audioManager.GetComponent<AudioManager>().sfxAudioSource.PlayOneShot(playerHurt, playerHurtVolume);
    }

    //Enemy Sounds
    void EnemyDie1()
    {
        audioManager.GetComponent<AudioManager>().sfxAudioSource.PlayOneShot(enemyDie[Random.Range(0,enemyDie.Length)], enemyDieVolume);   
    }

    void EnemyDie2()
    {
        audioManager.GetComponent<AudioManager>().sfxAudioSource.PlayOneShot(enemyDie2, EnemyDie2Volume);
    }

    void EnemyDie3()
    {
        audioManager.GetComponent<AudioManager>().sfxAudioSource.PlayOneShot(enemyDie3, EnemyDie3Volume);
    }

    //Pickups

    void CottonPickup()
    {
        audioManager.GetComponent<AudioManager>().sfxAudioSource.PlayOneShot(cottonPickup, cottonPickupVolume);
    }

}
