using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public Sprite[] enemySprites;
    public int randomSprites;
    // Start is called before the first frame update
    void Start()
    {
        randomSprites = Random.Range(0, 3);
        GetComponent<SpriteRenderer>().sprite = enemySprites[randomSprites];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
