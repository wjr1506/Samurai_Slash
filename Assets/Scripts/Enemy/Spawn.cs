using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float minTime;
    public float maxTime;
    float spawTime;
    public GameObject enemyPrefab;

    public float timeCount;
    // Start is called before the first frame update
    void Start()
    {
        spawTime = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if(timeCount >= spawTime)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation);

            spawTime = Random.Range(minTime, maxTime);
            timeCount = 0f;
        }
    }
}
