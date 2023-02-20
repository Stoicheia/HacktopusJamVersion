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
        protected override void Start()
        {
            base.Start();
            startPos = transform.position;
            endPos = endObject.position;
            isAlive = true;
        }
        private void Update()
        {
            if(isAlive == true)
            {
                float currentDistance = transform.position.y - endPos.y;
                float endDistance = startPos.y - endObject.position.y;
                SetProgress((float) 1 - (currentDistance / endDistance));
                if (Input.GetKey(_pressKey))
                {
                    transform.Translate(Vector2.down * (Time.deltaTime * rodSpeed));
                }
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
