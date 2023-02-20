using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Minigame.Games
{
    public class LetterListener : MinigameGameplay
    {
        public List<LetterDecider> letters = new List<LetterDecider>();
        public bool isListening;
        public List<char> selectedString;
        public float completion;
        public float countdown;

        public TextMeshProUGUI Instructional;

        private List<KeyCode> _listenToTheseKeycodes;
        public AudioSource typingSource;

        protected override void Start()
        {
            base.Start();
            isListening = false;
            selectedString = null;
            completion = 0f;
            countdown = 0.999f;
            SetProgress(countdown);

            _listenToTheseKeycodes = new List<KeyCode>()
            {
                KeyCode.Z,
                KeyCode.X,
                KeyCode.C,
                KeyCode.V,
                KeyCode.B,
                KeyCode.N,
                KeyCode.M
            };
            StartCoroutine(StartSequence(0.6f));
        }

        void Update()
        {
            if(letters.Capacity > 0)
            {
                if(letters[0].selectedLetter != null && selectedString == null)
                {
                    AssignString();
                }
            }


            if(isListening == true)
            {
                countdown -= Time.deltaTime/10;
                SetProgress(countdown);
                foreach(KeyCode vKey in _listenToTheseKeycodes)
                {
                    if(_inputs.GetKeyDown(vKey))
                    {
                        if(vKey.ToString() == selectedString[0].ToString())
                        {
                            Debug.Log("Letter Pressed");
                            letters[0].text.color = Color.green;
                            letters[0].text.text = letters[0].selectedLetter;
                            letters.RemoveAt(0);
                            selectedString.RemoveAt(0);
                            typingSource.Play();
                            completion += 1f;
                            if(completion == 3f)
                            {
                                SetProgress(1f);
                            }
                        }
                        else
                        {
                            letters[0].text.color = Color.red;
                            letters[0].text.text = vKey.ToString().ToUpper();
                            isListening = false;
                            StartCoroutine(FailSequence(0.4f));
                        }
                    }
                }
            }

            if(countdown <= 0)
            {
                Fail();
            }

        }

        void AssignString()
        {
            selectedString = new List<char>(letters[0].selectedLetter.ToUpper() + letters[1].selectedLetter.ToUpper() + letters[2].selectedLetter.ToUpper());
            isListening = true;
        }

        private IEnumerator FailSequence(float s)
        {
            yield return new WaitForSeconds(s);
            Fail();
        }

        private IEnumerator StartSequence(float s)
        {
            yield return new WaitForSeconds(s);
            Instructional.text = "PRESS IN ORDER";
        }
    }
}
