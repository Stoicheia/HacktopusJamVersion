using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Games
{
    public class ColorChange : MinigameGameplay
    {
        [Header("Controls")] [SerializeField] private KeyCode _pressKey;
        private float randomTimer;
        private SkewedImage _img;
        protected override void Start()
        {
            base.Start();
        }

        void Awake()
        {
            randomTimer = Random.Range(3f, 9f);
            _img = GetComponent<SkewedImage>();
        }
        private void Update()
        {
            randomTimer -= Time.deltaTime;
            if(randomTimer <= 0)
            {
                _img.color = new Color32(0, 82, 15, 255);
                if(_inputs.GetKeyDown(_pressKey))
                {
                    SetProgress(1f);
                }
            }
        }
    }
}