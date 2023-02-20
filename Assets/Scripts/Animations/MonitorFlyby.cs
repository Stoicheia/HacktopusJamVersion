using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Animations
{
    public class MonitorFlyby : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _flybyText;
        [SerializeField][TextArea(2,8)] private List<string> _sentences;
        [SerializeField] private RectTransform _startPosition;
        [SerializeField] private RectTransform _endPosition;
        [SerializeField] private float _scrollSpeed;

        private bool _started;

        public void Begin()
        {
            _started = true;
            ResetSentence();
        }

        public void Stop()
        {
            _started = false;
        }

        private void Update()
        {
            if (!_started) return;
            _flybyText.transform.Translate(_scrollSpeed * Vector3.left * Time.deltaTime,Space.Self);
            if (_flybyText.rectTransform.anchoredPosition.x + _flybyText.rectTransform.rect.width
                < _endPosition.anchoredPosition.x)
            {
                ResetSentence();
            }
        }

        private void ResetSentence()
        {
            _flybyText.text = _sentences[Random.Range(0, _sentences.Count)];
            _flybyText.transform.position = _startPosition.position ;
        }
    }
}