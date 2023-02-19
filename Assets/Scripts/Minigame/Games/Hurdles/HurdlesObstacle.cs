using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Games
{
    [RequireComponent(typeof(Collider2D))]
    public class HurdlesObstacle : MonoBehaviour
    {
        private Collider2D _col;
        private SkewedImage _img;
        public List<Sprite> sprites = new List<Sprite>();

        private void Awake()
        {
            _col = GetComponent<Collider2D>();
            _img = GetComponent<SkewedImage>();
            StartCoroutine(SpriteAnimation());
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            HurdlesPlayer hp = col.gameObject.GetComponent<HurdlesPlayer>();
            if(hp != null) hp.Die();
        }

        IEnumerator SpriteAnimation()
        {
            _img.sprite = sprites[0];
            yield return new WaitForSeconds(0.1f);
            _img.sprite = sprites[1];
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(SpriteAnimation());
        }
    }
}