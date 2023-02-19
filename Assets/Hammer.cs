using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hammer : MonoBehaviour
{

    public List<Sprite> sprites = new List<Sprite>();
    public Image rend;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Key Down");
            rend.sprite = sprites[1];
        }
        else
        {
            rend.sprite = sprites[0];
        }
    }
}
