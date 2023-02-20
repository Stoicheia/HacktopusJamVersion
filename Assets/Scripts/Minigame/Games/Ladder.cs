using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Games
{
    public class Ladder : MinigameGameplay
    {
        [Header("Controls")] [SerializeField] private KeyCode _pressKey;
        [Header("Audio")] [SerializeField] private AudioSource _audio;

        public Vector2 startPos;
        public Vector2 endPos;
        public RectTransform endObject;
        public float climbSpeed;

        private float endLocation => endObject.position.y;

        public float frameLength;
        public float counter;
        public float direction;
        public int frameCount;
        public List<Sprite> frames = new List<Sprite>();
        public Image _img;
        protected override void Start()
        {
            base.Start();
            startPos = transform.position;
            endPos = endObject.position;
        }
        private void Update()
        {
            float currentDistance = transform.position.y - endPos.y;
            float endDistance = startPos.y - endObject.position.y;
            SetProgress((float) 1 - (currentDistance / endDistance));
            if (Input.GetKey(_pressKey))
            {
                transform.Translate(Vector2.up * direction * (Time.deltaTime * climbSpeed));
                if(counter > 0)
                {
                    counter -= Time.deltaTime;
                }
                else
                {
                    counter = frameLength;
                    if(frameCount < 1)
                    {
                        frameCount += 1;
                    }
                    else
                    {
                        frameCount = 0;
                        _audio.PlayOneShot(_audio.clip);
                    }
                    _img.sprite = frames[frameCount];
                }
            }
        }
    }
}
