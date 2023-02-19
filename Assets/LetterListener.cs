using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Games
{
    public class LetterListener : MinigameGameplay
    {
        public List<LetterDecider> letters = new List<LetterDecider>();
        public bool isListening;
        public List<char> selectedString;
        public float completion;

        void Start()
        {
            isListening = false;
            selectedString = null;
            completion = 0f;
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
                foreach(KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if(Input.GetKeyDown(vKey))
                    {
                        if(vKey.ToString() == selectedString[0].ToString())
                        {

                            letters[0].text.color = Color.green;
                            letters[0].text.text = letters[0].selectedLetter;
                            letters.RemoveAt(0);
                            selectedString.RemoveAt(0);
                            completion += 1f;
                            //SetProgress((float)completion/3);
                        }
                    }
                }
            }

        }

        void AssignString()
        {
            selectedString = new List<char>(letters[0].selectedLetter.ToUpper() + letters[1].selectedLetter.ToUpper() + letters[2].selectedLetter.ToUpper());
            isListening = true;
        }
    }
}
