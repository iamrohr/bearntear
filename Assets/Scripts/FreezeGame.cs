using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeGame : MonoBehaviour
{
    public IEnumerator Freeze(float freezeInSecs)
    {
        Time.timeScale = 0; // paused

        yield return new WaitForSeconds(freezeInSecs);
        
        Time.timeScale = 1; // not paused

        yield return new WaitForSeconds(0.1f);
    }
}
