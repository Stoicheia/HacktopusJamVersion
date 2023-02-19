using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{

    public GameObject fishPrefab;
    public float randomTimer;
    public bool isLeft;

    void Start()
    {
        NewRandomTimer(0f, 1f);
    }
    void Update()
    {
        if(randomTimer > 0)
        {
            randomTimer -= Time.deltaTime;
        }
        else
        {
            SpawnFish();
            NewRandomTimer(2f, 6f);
        }
    }

    void SpawnFish()
    {
        GameObject newFish = Instantiate(fishPrefab, new Vector2(this.transform.position.x, this.transform.position.y + Random.Range(-0.5f, 0.5f)), Quaternion.identity);
        if(isLeft)
        {
            newFish.GetComponent<Fish>().fishSpeed = -0.9f;
        }
        else
        {
            newFish.GetComponent<Fish>().fishSpeed = 0.9f;
        }
        
        newFish.transform.SetParent(this.transform);

    }
    void NewRandomTimer(float first, float last)
    {
        randomTimer = Random.Range(first, last);
    }
}
