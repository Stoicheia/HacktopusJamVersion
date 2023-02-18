using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame
{
    public class Minigame : MonoBehaviour
    {
        public static event Action<Minigame> OnComplete;
        public static event Action<Minigame> OnFail;

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
        
        public Minigame Prefab { get; set; }
        
        [field: SerializeField] public MinigameGameplay Game { get; set; }

        [Header("Transforms")] [SerializeField]
        private Canvas _gameCanvas;
        [SerializeField] private SkewedImage _ui;
        [SerializeField] private Transform _gameplay;
        [SerializeField] private SkewedImage _progressBar;
        [SerializeField] private SkewedImage _controls;

        [Header("Graphics")] 
        [SerializeField] private List<SkewedImage> _allUIImages;
        [SerializeField] private List<SkewedText> _allUIText;
        [SerializeField] private Image _progressIndicator;

        private void Awake()
        {
            Game.SetGame(this);
            Progress = 0;
        }


        public void SetBounds(GameLayout layout)
        {
            CopySkewTransform(_ui, layout.UI);
            CopyTransform(_gameplay, layout.Game);
            //CopySkewTransform(_progressBar, layout.Progress);
            //CopySkewTransform(_controls, layout.InstructionPanel);
            SetAllSkews(_ui.skewX, _ui.skewY);
        }

        public void InvokeComplete()
        {
            OnComplete?.Invoke(this);
        }

        public void InvokeFail()
        {
            OnFail?.Invoke(this);
        }

        private void CopyTransform(Transform from, Transform to)
        {
            from.position = to.position;
            from.localScale = to.localScale;
            from.rotation = to.rotation;
        }

        private void CopySkewTransform(SkewedImage fromI, SkewedImage toI)
        {
            RectTransform from = fromI.GetComponent<RectTransform>();
            RectTransform to = toI.GetComponent<RectTransform>();
            
            from.position = to.position;
            from.localScale = to.localScale;
            from.rotation = to.rotation;

            from.sizeDelta = new Vector2(to.rect.width, to.rect.height);
            fromI.skewX = toI.skewX;
            fromI.skewY = toI.skewY;
        }

        public void SetAllSkews(float x, float y)
        {
            foreach (var s in _allUIImages)
            {
                s.skewX = x;
                s.skewY = y;
            }

            foreach (var s in _allUIText)
            {
                s.skewX = x;
                s.skewY = y;
            }
        }

        public void SetCamera(Camera c)
        {
            _gameCanvas.worldCamera = c;
        }
    }
}
