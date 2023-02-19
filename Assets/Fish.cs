using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float fishSpeed;
    public float deathTimer = 10f;
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.left * (Time.deltaTime * fishSpeed));
        deathTimer -= Time.deltaTime;
        if(deathTimer < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
