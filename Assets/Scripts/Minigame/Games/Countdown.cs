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

        public RectTransform timeOverlay;

        public Text finalCdText;
        public float finalTiming = 1.6f;
        private float finalTimer = 1.6f;

        public float RushTime = 3;
        public float RushThreshold = 0.85f;

        private void Start()
        {
            timeOverlay.gameObject.SetActive(false);
            bgRend = bg.GetComponent<SkewedImage>();
            bgRend.color = countdownColor;
            isListening = false;
            SetProgress((float)completion);
            finalCdText.gameObject.SetActive(false);
            finalTimer = finalTiming;
        }
        private void Update()
        {
            if (isListening) finalTimer -= Time.deltaTime;
            finalCdText.text = $"{finalTimer : 0.0}s";
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
                float rushFactor = 1;
                if (_minigame.Manager.Progress > RushThreshold) rushFactor = RushTime;
                timeRemaining -= Time.deltaTime * rushFactor;
                completion -= Time.deltaTime * rushFactor/10; 
                SetProgress((float)completion);
                var dots = "";
                for (int i = 0; i < timeRemaining % 3; i++)
                {
                    dots += ".";
                }
                tutorialText.text = $"WAIT{dots}";
            }
            else
            {
                timeRemaining = 1;
            }

            DisplayTime(timeRemaining);

            if(timeRemaining == 1)
            {
                tutorialText.text = "PRESS L";
                StartCoroutine(pressKeyWindow());
            }
        }

        void DisplayTime(float timeToDisplay)
        {
            Debug.Log("Changing Display");
            if(timeToDisplay < 1)
            {
                timeToDisplay = 1;
            }

            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timerText.text = string.Format("{0}", seconds);
        }

        IEnumerator pressKeyWindow()
        {
            finalCdText.gameObject.SetActive(true);
            timeOverlay.gameObject.SetActive(true);
            bgRend.color = pressKeyColour;
            timerText.color = pressKeyColour;
            downloadingText.color = pressKeyColour;
            tutorialText.color = pressKeyColour;
            isListening = true;
            yield return new WaitForSeconds(finalTiming);
            SetProgress((float)0);
            Fail();
        }
    }
}