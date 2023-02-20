using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderSFX : MonoBehaviour
{
    public 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            transform.GetComponent<AudioSource>().Play();
        }

        if(Input.GetKeyUp(KeyCode.G))
        {
            transform.GetComponent<AudioSource>().Stop();
        }
        
    }
}
