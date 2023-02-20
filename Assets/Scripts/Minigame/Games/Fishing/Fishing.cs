using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Games
{
    public class Fishing : MinigameGameplay
    {
        [Header("Controls")] [SerializeField] private KeyCode _pressKey;

        public Vector2 startPos;
        public Vector2 endPos;
        public RectTransform endObject;
        public float rodSpeed;
        public bool isAlive;

        private float endLocation => endObject.position.y;
        private float _progression;
        protected override void Start()
        {
            base.Start();
            startPos = transform.position;
            endPos = new Vector2(startPos.x, endObject.position.y);
            isAlive = true;
        }
        private void Update()
        {
            if(isAlive == true)
            {
                if (Input.GetKey(_pressKey))
                {
                    _progression += rodSpeed * Time.deltaTime;
                    transform.position = Vector2.Lerp(startPos, endPos, _progression);
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
