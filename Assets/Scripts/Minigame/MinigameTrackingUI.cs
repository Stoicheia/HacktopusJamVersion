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

        
    }
}