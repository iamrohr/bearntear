using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlash : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer;

    public IEnumerator Flash()
    {
        playerSpriteRenderer.color = Color.red; // red

        yield return new WaitForSeconds(0.5f);

        playerSpriteRenderer.color = Color.white; // red
    }

        
    
}
