using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimator : MonoBehaviour
{
    public float frameLength;
    public float counter;
    public int frameCount;
    public List<Sprite> frames = new List<Sprite>();
    public Image _img;
    void Update()
    {
        if(counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            counter = frameLength;
            if(frameCount < 3)
            {
                frameCount += 1;
            }
            else
            {
                frameCount = 0;
            }
            _img.sprite = frames[frameCount];
        }
    }
}
