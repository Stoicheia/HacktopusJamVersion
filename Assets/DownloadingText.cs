using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownloadingText : MonoBehaviour
{
    public Text text;
    private string[] textOptions = {"Downloading", "Downloading.", "Downloading..", "Downloading..."};
    private int cycler;
    private float counter;
    public float initialCounter;

    void Start()
    {
        cycler = 0;
        counter = initialCounter;
    }

    // Update is called once per frame
    void Update()
    {
        if(counter > 0)
        {
            counter -= Time.deltaTime;
        }
        
        if(counter <= 0)
        {
            counter = initialCounter;
            if(cycler < 3)
            {
                cycler += 1;
            }
            else
            {
                cycler = 0;
            }
            text.text = textOptions[cycler];
        }
    }
}
