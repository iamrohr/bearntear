using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlash : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer;

    public IEnumerator Flash(float WaitForSeconds, int NrTimesToRunScript)
    {
        for (int i = 0; i < NrTimesToRunScript; i++)
        {
            playerSpriteRenderer.color = Color.red; // red

            yield return new WaitForSeconds(WaitForSeconds);

            playerSpriteRenderer.color = Color.white; // red
        }
    }
}
