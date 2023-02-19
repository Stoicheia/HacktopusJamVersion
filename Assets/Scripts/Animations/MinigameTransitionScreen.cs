using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Animations
{
    public class MinigameTransitionScreen : MonoBehaviour
    {
        public event Action OnEnd;
        
        [Header("Elements")] [SerializeField] private RectTransform _middle;
        [SerializeField] private Image _background;
        [Header("Tween")] [SerializeField] private float _entryDuration;
        [SerializeField] private float _exitDuration;

        [SerializeField] private float _rotationMagnitude;
        [SerializeField] private float _alphaFadeSeconds;
        [SerializeField] private float _rotateSeconds;
        [SerializeField] private float _waitSeconds;
        [SerializeField] private Color _backgroundColor;

        [Header("Debug")][SerializeField] private bool _debugMode;

        private bool _isPlaying;

        private void Start()
        {
            
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space) && !_isPlaying && _debugMode) Play();
        }

        public void Play()
        {
            _isPlaying = true;
            gameObject.SetActive(true);
            _background.color = _backgroundColor;
            _middle.transform.localRotation = Quaternion.Euler(0,0,_rotationMagnitude);
            _middle.transform.localScale = new Vector3(0, 0, 0);
            var _sequence = DOTween.Sequence();
            //_sequence.Join(_background.DOColor(_backgroundColor, _alphaFadeSeconds));
            _sequence.Join(_middle.transform.DOLocalRotate(new Vector3(0,0,0),_rotateSeconds));
            _sequence.Join(_middle.transform.DOScale(new Vector3(1,1,1), _entryDuration).SetEase(Ease.OutElastic));
            _sequence.AppendInterval(_waitSeconds);
            _sequence.Append(_middle.transform.DOScale(new Vector3(0,0,0), _exitDuration));
            _sequence.Join(_middle.transform.DOLocalRotate(new Vector3(0, 0, _rotationMagnitude), _rotateSeconds)).SetDelay(_exitDuration-_rotateSeconds);
            //_sequence.Join(_background.DOColor(new Color(0, 0, 0, 0), _alphaFadeSeconds)).SetDelay(_exitDuration-_alphaFadeSeconds);
            _sequence.onComplete += () =>
            {
                OnEnd?.Invoke();
                _isPlaying = false;
                if (!_debugMode)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("Done");
                }
            };
            _sequence.Play();
        }
    }
}