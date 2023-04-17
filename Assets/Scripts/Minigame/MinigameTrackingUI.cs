using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame
{
    public class MinigameTrackingUI : MonoBehaviour
    {
        [SerializeField] private MinigameManager _manager;

        [Header("Graphics")] [SerializeField] private TextMeshProUGUI _timerField;
        [SerializeField] private TextMeshProUGUI _progressField;
        [SerializeField] private TextMeshProUGUI _progressPercentField;
        [SerializeField] private Image _progressImage;

        private void Update()
        {
            _timerField.text = $"{(int)_manager.Timer / 60:00}:{(int)_manager.Timer % 60:00}";
            _progressField.text = $"{_manager.MinigamesCompleted}/{_manager.MinigamesCount}";
            _progressPercentField.text = $"{(float)_manager.MinigamesCompleted/_manager.MinigamesCount:0%}";
            _progressImage.fillAmount = (float) _manager.MinigamesCompleted / _manager.MinigamesCount;
        }
    }
}