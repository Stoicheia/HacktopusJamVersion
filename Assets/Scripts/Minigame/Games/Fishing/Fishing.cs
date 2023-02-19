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
        public float endDistance;
        public float rodSpeed;
        public bool isAlive;
        protected override void Start()
        {
            base.Start();
            startPos = transform.position;
            endPos = new Vector2(startPos.x, startPos.y - endDistance);
            isAlive = true;
        }
        private void Update()
        {
            if(isAlive == true)
            {
                float currentDistance = transform.position.y - endPos.y;
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
