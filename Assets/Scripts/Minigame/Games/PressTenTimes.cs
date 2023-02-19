using System;
using TMPro;
using UnityEngine;

namespace Minigame.Games
{
    public class PressTenTimes : MinigameGameplay
    {
        [Header("Controls")] [SerializeField] private KeyCode _pressKey;

        [Header("Config")] [SerializeField] private int _targetPresses;

        [SerializeField] private TextMeshProUGUI _pressesLeftField;

        private int _currentPresses;
        protected override void Start()
        {
            base.Start();
        }
        private void Update()
        {
            if (_inputs.GetKeyDown(_pressKey))
            {
                _currentPresses++;
                SetProgress((float)_currentPresses/_targetPresses);
            }

            _pressesLeftField.text = (_targetPresses - _currentPresses).ToString();
        }
    }
}