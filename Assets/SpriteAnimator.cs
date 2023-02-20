using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Games
{
    public class SpriteAnimator : MinigameGameplay
    {
        [Header("Controls")] [SerializeField] private KeyCode _animKey;
        public float frameLength;
        public float counter;
        public int frameCount;
        public List<Sprite> frames = new List<Sprite>();
        public Image _img;

        public bool isKeyBound;

        protected override void Start()
        {
            base.Start();
        }
        void Update()
        {
            if(isKeyBound == true && _inputs.GetKeyDown(_animKey))
            {
                AnimateSprite();
            }
            else if(isKeyBound == true && !_inputs.GetKeyDown(_animKey))
            {

            }
            else
            {
                AnimateSprite();
            }
        }

        void AnimateSprite()
        {
            if(counter > 0)
            {
                counter -= Time.deltaTime;
            }
            else
            {
                counter = frameLength;
                if(frameCount < frames.Capacity - 1)
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
}
