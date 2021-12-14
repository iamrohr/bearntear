using System;
using System.Collections;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField] private float timeOnScreen, fadeTime;
    

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();        
    }

    private void Start()
    {
        StartCoroutine(FadeCoroutine());
    }
    
    private IEnumerator FadeCoroutine()
    {
        float t = 0;
        while (t < 1)
        {
            float alpha = 1 - (1 - t) * (1 - t) * (1 - t);
            canvasGroup.alpha = alpha;
            t += Time.deltaTime / fadeTime;
            yield return null;
        }

        yield return new WaitForSeconds(timeOnScreen);
        
        t = 0;
        while (t < 1)
        {
            float alpha = 1 - t * t;
            canvasGroup.alpha = alpha;
            t += Time.deltaTime / fadeTime;
            yield return null;
        }

        Destroy(gameObject);
        yield return null;
    }
}
