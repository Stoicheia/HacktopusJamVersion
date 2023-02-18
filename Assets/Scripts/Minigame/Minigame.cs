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

        private float Progress
        {
            get => _progress;
            set
            {
                if(value < 0) Debug.LogWarning("Invalid progress value. Clamping.");
                if(value >= 1) InvokeComplete();
                _progress = Mathf.Clamp01(value);
            }
        }

        [Header("Transforms")]
        [SerializeField] private RectTransform _ui;
        [SerializeField] private Transform _gameplay;

        [Header("UI")] 
        [SerializeField] private Slider _progressBar;
        [SerializeField] private RectTransform _controls;

        public void SetBounds(RectTransform ctrlT, Transform gameT)
        {
            _ui.position = ctrlT.position;
            _ui.localScale = ctrlT.localScale;
            _ui.rotation = ctrlT.rotation;

            _gameplay.position = gameT.position;
            _gameplay.localScale = gameT.localScale;
            _gameplay.rotation = gameT.rotation;
        }

        public void InvokeComplete()
        {
            OnComplete?.Invoke();
        }

        public void InvokeFail()
        {
            OnFail?.Invoke();
        }
    }
}
