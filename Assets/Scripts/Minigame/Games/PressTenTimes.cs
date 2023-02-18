using System;
using UnityEngine;

namespace Minigame.Games
{
    public class PressTenTimes : MinigameGameplay
    {
        [Header("Controls")] [SerializeField] private KeyCode _pressKey;

        [Header("Config")] [SerializeField] private int _targetPresses;

        private int _currentPresses;
        private void Start()
        {
            
        }
        private void Update()
        {
            if (Input.GetKeyDown(_pressKey))
            {
                _currentPresses++;
                SetProgress((float)_currentPresses/_targetPresses);
            }
        }
    }
}