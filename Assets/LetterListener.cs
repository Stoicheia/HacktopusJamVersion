using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Games
{
    public class LetterListener : MinigameGameplay
    {
        private static LetterListener ACTIVE_LISTENER;
        private static bool TRANSFER_LOCK;
        
        public List<LetterDecider> letters = new List<LetterDecider>();
        public bool isListening;
        public List<char> selectedString;
        public float completion;
        public float countdown;

        public TextMeshProUGUI Instructional;

        private List<KeyCode> _listenToTheseKeycodes;
        public AudioSource typingSource;

        private bool _ended;

        private bool _active => ACTIVE_LISTENER == this;

        [SerializeField] private Image _activeIndicator;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;

        protected override void Start()
        {
            _ended = false;
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
            _activeIndicator.color = ACTIVE_LISTENER == this ? _activeColor : _inactiveColor;
            if (TRANSFER_LOCK) _activeIndicator.color = _activeColor;
            if (!_ended && ACTIVE_LISTENER == null && !TRANSFER_LOCK)
            {
                StartCoroutine(TransferOwnership(2));
            }
            if(letters.Capacity > 0)
            {
                if(letters[0].selectedLetter != null && selectedString == null)
                {
                    AssignString();
                }
            }


            if(isListening && _active)
            {
                countdown -= Time.deltaTime/10;
                SetProgress(countdown);
                foreach(KeyCode vKey in _listenToTheseKeycodes)
                {
                    if(_inputs.GetKeyDown(vKey, _minigame))
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
                                Defer();
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

        private void Defer()
        {
            ACTIVE_LISTENER = null;
            _ended = true;
        }

        private IEnumerator FailSequence(float s)
        {
            Defer();
            yield return new WaitForSeconds(s);
            Fail();
        }

        private IEnumerator StartSequence(float s)
        {
            yield return new WaitForSeconds(s);
            Instructional.text = "PRESS IN ORDER";
        }

        private IEnumerator TransferOwnership(int frames)
        {
            TRANSFER_LOCK = true;
            for (int i = 0; i < frames; i++)
            {
                yield return null;
            }
            TRANSFER_LOCK = false;
            ACTIVE_LISTENER = this;
        }
    }
}
