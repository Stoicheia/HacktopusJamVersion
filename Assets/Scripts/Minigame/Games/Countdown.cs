using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Games
{
    public class Countdown : MinigameGameplay
    {
        [Header("Controls")] [SerializeField] private KeyCode _pressKey;

        public float timeRemaining = 11f;
        public Text timerText;
        public GameObject bg;
        private Renderer bgRend;
        private Color countdownColor = Color.white;
        private Color pressKeyColour = Color.green;
        private bool isListening;

        private void Start()
        {
            bgRend = bg.GetComponent<Renderer>();
            bgRend.material.color = countdownColor;
            isListening = false;
            SetProgress((float)0.5);
        }
        private void Update()
        {
            if(isListening == false)
            {
                if(Input.GetKeyDown(_pressKey))
                {
                    SetProgress((float)0);
                }
            }
            else
            {
                if(Input.GetKeyDown(_pressKey))
                {
                    SetProgress((float)1);
                }
            }

            if(timeRemaining > 1)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 1;
            }

            DisplayTime(timeRemaining);

            if(timeRemaining == 1)
            {
                StartCoroutine(pressKeyWindow());
            }
        }

        void DisplayTime(float timeToDisplay)
        {
            if(timeToDisplay < 1)
            {
                timeToDisplay = 1;
            }

            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timerText.text = string.Format("{0}", seconds);
        }

        IEnumerator pressKeyWindow()
        {
            bgRend.material.color = pressKeyColour;
            timerText.color = pressKeyColour;
            isListening = true;
            yield return new WaitForSeconds(1);
            SetProgress((float)0);
        }
    }
}