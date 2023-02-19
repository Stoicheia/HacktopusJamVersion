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
        public float completion = 0.999f;
        public Text timerText;
        public Text downloadingText;
        public Text tutorialText;
        public GameObject bg;
        private SkewedImage bgRend;
        private Color countdownColor = Color.white;
        private Color pressKeyColour = Color.green;
        private bool isListening;

        private void Start()
        {
            bgRend = bg.GetComponent<SkewedImage>();
            bgRend.color = countdownColor;
            isListening = false;
            SetProgress((float)completion);
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
                completion -= Time.deltaTime/10;
                SetProgress((float)completion);
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
            bgRend.color = pressKeyColour;
            timerText.color = pressKeyColour;
            downloadingText.color = pressKeyColour;
            tutorialText.color = pressKeyColour;
            isListening = true;
            yield return new WaitForSeconds(1);
            SetProgress((float)0);
        }
    }
}