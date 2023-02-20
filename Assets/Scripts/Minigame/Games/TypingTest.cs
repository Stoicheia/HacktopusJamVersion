using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace Minigame.Games
{
    public class TypingTest : MinigameGameplay
    {
        [SerializeField] [TextArea(2,8)] private List<string> _possibleSentences;
        [SerializeField] private TextMeshProUGUI _textArea;
        private string _activeSentence;
        private int _ptr;

        protected override void Start()
        {
            base.Start();
            _activeSentence = _possibleSentences[UnityEngine.Random.Range(0, _possibleSentences.Count)];
        }

        private void Update()
        {
            char current = _activeSentence[_ptr];
            string inputString = Input.inputString;
            for (int i = 0; i < inputString.Length; i++)
            {
                if (!String.Equals(inputString[i].ToString(), current.ToString(), StringComparison.CurrentCultureIgnoreCase)) break;
                _ptr++;
                if (_ptr >= _activeSentence.Length) break;
                current = _activeSentence[_ptr];
            }

            if (current == ' ')
            {
                _ptr++;
            }
            
            SetProgress((float)_ptr / _activeSentence.Length);

            if (_ptr >= _activeSentence.Length)
            {
                _textArea.text = "COMPLETE!";
            }
            else
            {
                _textArea.text =
                    $"<color=green>{_activeSentence.Substring(0, _ptr)}</color><color=red>{_activeSentence[_ptr]}</color>{_activeSentence.Substring(_ptr + 1)}";
            }
        }
        
    }
}