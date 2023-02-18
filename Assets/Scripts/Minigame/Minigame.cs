using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame
{
    public class Minigame : MonoBehaviour
    {
        public event Action OnComplete;
        public event Action OnFail;

        private float _progress;

        public float Progress
        {
            get => _progress;
            set
            {
                if(value < 0) Debug.LogWarning("Invalid progress value. Clamping.");
                if(value >= 1) InvokeComplete();
                _progress = Mathf.Clamp01(value);
                _progressIndicator.fillAmount = value;
            }
        }
        
        [field: SerializeField] public MinigameGameplay Game { get; set; }

        [Header("Transforms")] [SerializeField]
        private Canvas _gameCanvas;
        [SerializeField] private RectTransform _ui;
        [SerializeField] private Transform _gameplay;
        [SerializeField] private RectTransform _progressBar;
        [SerializeField] private RectTransform _controls;

        [Header("Graphics")] 
        [SerializeField] private Image _progressIndicator;
        

        public void SetBounds(GameLayout layout)
        {
            CopyTransform(_ui, layout.UI);
            CopyTransform(_gameplay, layout.Game);
            CopyTransform(_progressBar, layout.Progress);
            CopyTransform(_controls, layout.InstructionPanel);
        }

        public void InvokeComplete()
        {
            OnComplete?.Invoke();
        }

        public void InvokeFail()
        {
            OnFail?.Invoke();
        }

        private void CopyTransform(Transform from, Transform to)
        {
            from.position = to.position;
            from.localScale = to.localScale;
            from.rotation = to.rotation;
        }

        public void SetCamera(Camera c)
        {
            _gameCanvas.worldCamera = c;
        }
    }
}
