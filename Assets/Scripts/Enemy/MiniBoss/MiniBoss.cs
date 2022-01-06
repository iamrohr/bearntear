using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniBoss : MonoBehaviour
{
    public GameObject backgroundMusic;
    public GameObject miniBossMusic;
    public GameObject swipeAttack;
    public GameObject swipeFinalAttack;

    GameObject elevatorBlocker;

    GameObject nextLevel;

    private void Awake()
    {
        miniBossMusic.SetActive(false);
    }

    private void Start()
    {
        nextLevel = GameObject.FindGameObjectWithTag("NextLevel");
        elevatorBlocker = GameObject.Find("ElevatorBlocker");
        //if (nextLevel != null)
        //{
        //    nextLevel.SetActive(false);
        //}
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("MiniBoss").Length > 0)
        {
            miniBossMusic.SetActive(true);
            backgroundMusic.SetActive(false);
            swipeAttack.GetComponent<PlayerAttackBox>().timeStunned = 0.1f;
            swipeAttack.GetComponent<PlayerAttackBox>().timeKnocked = 0.05f;
            swipeAttack.GetComponent<PlayerAttackBox>().knockBackPower = 50f;

            swipeFinalAttack.GetComponent<PlayerAttackBox>().knockBackPower = 200f;
            swipeFinalAttack.GetComponent<PlayerAttackBox>().timeKnocked = 0.1f;
            swipeFinalAttack.GetComponent<PlayerAttackBox>().timeStunned = 0.5f;
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("RoofTop"))
            {
                elevatorBlocker.SetActive(true);

            }

            //if (nextLevel != null)
            //{
            //    nextLevel.SetActive(false);
            //}
        }

        if (GameObject.FindGameObjectsWithTag("MiniBoss").Length <= 0 && Time.timeScale == 1)
        {
            miniBossMusic.SetActive(false);
            backgroundMusic.SetActive(true);
            swipeAttack.GetComponent<PlayerAttackBox>().timeStunned = 0.25f;
            swipeAttack.GetComponent<PlayerAttackBox>().timeKnocked = 0.1f;
            swipeAttack.GetComponent<PlayerAttackBox>().knockBackPower = 100f;

            swipeFinalAttack.GetComponent<PlayerAttackBox>().knockBackPower = 600f;
            swipeFinalAttack.GetComponent<PlayerAttackBox>().timeKnocked = 1.5f;
            swipeFinalAttack.GetComponent<PlayerAttackBox>().timeStunned = 1f;

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("RoofTop"))
            {
                elevatorBlocker.SetActive(false);

            }

            //if (nextLevel != null)
            //{
            //    nextLevel.SetActive(true);
            //}
        }
    }
}
