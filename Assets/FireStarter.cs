using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStarter : MonoBehaviour
{
    public GameObject fireParticleSystemGameObject;
    public ParticleSystem fireParticleSystem;

    void Start()
    {
        fireParticleSystemGameObject.SetActive(true);
        fireParticleSystem.Play(true);
    }
}
