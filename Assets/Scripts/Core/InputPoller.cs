using System;
using UnityEngine;

namespace Minigame.Games.Core
{
    public class InputPoller : MonoBehaviour
    {
        public bool InputsEnabled { get; set; }

        private void Start()
        {
            //InputsEnabled = true;
        }

        public bool GetKeyDown(KeyCode k)
        {
            return (InputsEnabled && Input.GetKeyDown(k));
        }
    }
}