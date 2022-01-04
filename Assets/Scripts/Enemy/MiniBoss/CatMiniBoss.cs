using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMiniBoss : MonoBehaviour
{
    public GameObject catMiniBoss;
    public GameObject player;

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            Instantiate(catMiniBoss, new Vector3(125.31f, -2.63f, 0f), player.transform.rotation);
        }
    }
}
