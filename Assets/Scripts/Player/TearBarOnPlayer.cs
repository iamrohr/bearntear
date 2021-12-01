using UnityEngine;

public class TearBarOnPlayer : MonoBehaviour
{
    public TearBar tearBar;

    public int maxTear = 100;
    public float currentTear;
    public int startTearLevel = 0;
   
    //Counter
    float timeElapsed = 100; //Count time down from 100 
    public float timeSpeed = 1; //Acceleration

    void Start()
    {
        currentTear = maxTear;
        tearBar.SetMaxTearLevel(100);
        RemoveTear(60);
    }

    void Update()
    {
        tearBar.SetTearLevel(startTearLevel);
        //decrease tear
        if (currentTear >= 0)
        { 
            currentTear -= timeSpeed * Time.deltaTime;
            tearBar.SetTearLevel((int)currentTear);
        }   
    }

    public void GetTear(int tear)
    {
        currentTear += tear;
        currentTear = Mathf.Clamp(currentTear, 0, 100);
        tearBar.SetTearLevel((int)currentTear);
    }

    public void RemoveTear(int tear)
    {
        currentTear -= tear;
        currentTear = Mathf.Clamp(currentTear, 0, 100);
        tearBar.SetTearLevel((int)currentTear); 
    }

}