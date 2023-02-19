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
        NewRandomTimer(1f, 3f);
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
            NewRandomTimer(3f, 6f);
        }
    }

    void SpawnFish()
    {
        GameObject newFish = Instantiate(fishPrefab, new Vector2(this.transform.position.x, Random.Range(this.transform.position.y + 50, this.transform.position.y - 50)), Quaternion.identity);
        if(isLeft)
        {
            newFish.GetComponent<Fish>().fishSpeed = -40f;
        }
        else
        {
            newFish.GetComponent<Fish>().fishSpeed = 40f;
        }
        
        newFish.transform.SetParent(this.transform);

    }
    void NewRandomTimer(float first, float last)
    {
        randomTimer = Random.Range(first, last);
    }
}
