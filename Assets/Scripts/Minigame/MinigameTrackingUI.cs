using System;
using TMPro;
using UnityEngine;

namespace Minigame
{
    public class MinigameTrackingUI : MonoBehaviour
    {
        [SerializeField] private MinigameManager _manager;

        [Header("Graphics")] [SerializeField] private TextMeshProUGUI _timerField;
        [SerializeField] private TextMeshProUGUI _progressField;

        private void Update()
        {
            _timerField.text = $"{_manager.Timer / 60:00}:{_manager.Timer % 60:00}";
            _progressField.text = $"{_manager.MinigamesCompleted}/{_manager.MinigamesCount}";
        }
    }
}