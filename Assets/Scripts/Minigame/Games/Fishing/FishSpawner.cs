using System.Collections;
using System.Collections.Generic;
using Minigame.Games;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    
    public GameObject fishPrefab;
    public float randomTimer;
    public bool isLeft;

    public Fishing fishing;

    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;

    void Start()
    {
        NewRandomTimer(0.1f, 1f);
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
            NewRandomTimer(2f, 5f);
        }
    }

    void SpawnFish()
    {
        GameObject newFish = Instantiate(fishPrefab, new Vector2(this.transform.position.x, Random.Range(bottom.position.y, top.position.y)), Quaternion.identity);
        float oldScale = fishPrefab.transform.localScale.x;
        float s = oldScale * fishing.Scale;
        newFish.transform.localScale = new Vector3(s, s, s);
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
