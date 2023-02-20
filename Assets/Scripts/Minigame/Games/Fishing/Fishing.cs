using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Games
{
    public class Fishing : MinigameGameplay
    {
        [Header("Controls")] [SerializeField] private KeyCode _pressKey;
        public AudioSource _audio;

        public Vector2 startPos;
        public Vector2 endPos;
        public RectTransform endObject;
        public float rodSpeed;
        public bool isAlive;

        [SerializeField] private List<FishSpawner> _spawners;

        private float endLocation => endObject.position.y;
        private float _progression;

        public float Scale => _scale;
        protected override void Start()
        {
            base.Start();
            startPos = transform.position;
            endPos = endObject.position;
            isAlive = true;
        }
        private void Update()
        {
            foreach (var s in _spawners)
            {
                s.fishing = this;
            }
            if(isAlive == true)
            {
                if (Input.GetKey(_pressKey))
                {
                    _progression += rodSpeed * Time.deltaTime;
                    transform.position = Vector2.Lerp(startPos, endPos, _progression);
                    Debug.Log("Playing Audio");
                }

                if(Input.GetKeyDown(_pressKey))
                {
                    _audio.PlayOneShot(_audio.clip);
                }

                SetProgress((float) _progression);
            }
            
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.gameObject.tag == "Fish")
            {
                Debug.Log("You died!");
                isAlive = false;
                SetProgress((float) 0);
                Fail();
            }
        }
    }
}
