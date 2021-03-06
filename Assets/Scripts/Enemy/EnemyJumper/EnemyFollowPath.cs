using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPath : MonoBehaviour
{

    [SerializeField] private Transform[] routes;
    [HideInInspector] public bool coroutineAllowed;
    private int routeToGo;
    private float tParam;
    private Vector2 objectPosition;
    private float speedModifier;

    [Header("Components")]
    [HideInInspector] public GameObject player;


    // Spawn Enemy
    public Transform enemy;

    // Start is called before the first frame update

    void Start()

    {
        player = GameObject.FindGameObjectWithTag("Player");

        routeToGo = 0;
        tParam = 0f;
        speedModifier = 1f;
        coroutineAllowed = false;

    }

    // Update is called once per frame

    void Update()

    {
        //Debug.Log("Coroutine is " + coroutineAllowed);
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    public IEnumerator GoByTheRoute(int routeNum)

    {
        coroutineAllowed = false;
        Vector2 p0 = routes[routeNum].GetChild(0).position;
        Vector2 p1 = routes[routeNum].GetChild(1).position;
        Vector2 p2 = routes[routeNum].GetChild(2).position;
        Vector2 p3 = routes[routeNum].GetChild(3).position;

        while (tParam < 1)

        {
            tParam += Time.deltaTime * speedModifier;
            objectPosition = Mathf.Pow(1 - tParam, 3) * 
                p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * 
                p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * 
                p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = objectPosition;

            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo += 1;

        if (routeToGo > routes.Length - 1)

        {
            routeToGo = 0;
        }

        coroutineAllowed = false;
        Instantiate(enemy, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        enemy.transform.localScale = new Vector3(1, 1, 1);
        Destroy(transform.parent.gameObject);
    }
}
