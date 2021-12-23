using System;
using UnityEngine;
using UnityEngine.UI;

public class TearBarOnPlayer : MonoBehaviour
{
    [NonSerialized] public bool tearBarFull;
    [NonSerialized] public bool pauseTearDecrease;
 
    public TearBar tearBar;
    public GameObject tearBarFill;

    public int maxTear = 100;
    public int startTearLevel = 0;
    public float currentTear;

    public float tearPauseTime;
    public int maxTearTime;

    //Counter
    public float timeSpeed = 1; //Acceleration

    public TearMode tearModeScript;
    public CameraShake cameraShake;

    void Start()
    {
        currentTear = maxTear;
        tearBar.SetMaxTearLevel(100);
        tearBar.SetTearLevel((int)maxTear);
        RemoveTear(100);
    }

    void Update()
    {
          //Decrease tear over time
        if (!tearBarFull)
        {
            if (currentTear >= 0 && !pauseTearDecrease)
            {
                if(!tearModeScript.tearModeOn)
                {
                    currentTear -= timeSpeed * Time.deltaTime;
                }
                
                tearBar.SetTearLevel((int)currentTear);
            }   
        }

        if(currentTear == maxTear)
        {
            TearBarFullLock();
        }
    }

    public void GetTear(int tear)
    {
        currentTear += tear;
        currentTear = Mathf.Clamp(currentTear, 0, 100);
        tearBar.SetTearLevel((int)currentTear);
        if (currentTear == maxTear)
        {
            // tearBarFill.GetComponent<Image>().color = Color.red;
        }
    }

    public void RemoveTear(int tear)
    {
        currentTear -= tear;
        currentTear = Mathf.Clamp(currentTear, 0, 100);
        tearBar.SetTearLevel((int)currentTear); 
    }

    public void TearDecreaseOff(int pauseTime)
    {
        pauseTearDecrease = true;
        CancelInvoke(nameof(TearDecreaseOn));
        Invoke(nameof(TearDecreaseOn), (float)pauseTime);
    }

    private void TearDecreaseOn()
    {
        pauseTearDecrease = false;
    }

    public void TearBarFullLock()
    {
        tearBarFull = true;
        CancelInvoke(nameof(TearBarFullUnlock));
        Invoke(nameof(TearBarFullUnlock), (float)maxTearTime);
    }

    public void TearBarFullUnlock()
    {
        tearBarFull = false;
    }
}