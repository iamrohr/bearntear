using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroFadeInOut : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void IntroFadeIn()
    {
        animator.SetTrigger("IntroFadeIn");
    }

    public void IntroFadeOut()
    {
        animator.SetTrigger("IntroFadeOut");
    }
}
