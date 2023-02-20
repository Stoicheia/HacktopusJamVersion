using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleAnimator : MonoBehaviour
{
    public float frameLength;
        public float counter;
        public int frameCount;
        public List<Sprite> frames = new List<Sprite>();
        public Image _img;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Y))
        {
            if(counter > 0)
            {
                counter -= Time.deltaTime;
            }
            else
            {
                counter = frameLength;
                if(frameCount < 2)
                {
                    frameCount += 1;
                }
                    else
                {
                    frameCount = 1;
                }
                _img.sprite = frames[frameCount];
                _img.color = new Color32(255, 255, 255, 255);
            }       
        }
        else
        {
            _img.sprite = null;
            _img.color = new Color32(0, 0, 0, 0);
        }
        
    }
}
