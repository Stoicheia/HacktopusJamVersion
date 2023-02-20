using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Minigame.Games.Identity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Minigame.Games
{
    public class IdentityTheft : MinigameGameplay
    {
        [SerializeField] private IdentityTarget _target;
        [SerializeField] private List<IdentityOption> _options;

        [SerializeField] private List<IdentityElement> _possibleFirst;
        [SerializeField] private List<IdentityElement> _possibleSecond;
        [SerializeField] private List<IdentityElement> _possibleThird;

        [SerializeField] private RectTransform _correctGraphic;

        private List<IdentityElement> _currentlySelected;
        private List<IdentityElement> _correct;

        protected override void Start()
        {
            base.Start();
            GenerateCorrect();
            _target.Load(_correct);
            _correctGraphic.gameObject.SetActive(false);
        }

        private void Update()
        {
            foreach (var o in _options)
            {
                if (_inputs.GetKeyDown(o.Key))
                {
                    o.Toggle();
                }
            }
            _currentlySelected = _options.Select(x => x.SelectedElement).ToList();
            float correct = _target.CheckCorrectness(_currentlySelected);
            if (correct >= 1)
            {
                SetProgress(0.99f);
                StartCoroutine(WinSequence(0.75f));
            }
            else
            {
                SetProgress(correct);
            }
        }

        private void GenerateCorrect()
        {
            List<IdentityElement> elements = new List<IdentityElement>();
            elements.Add(_possibleFirst[Random.Range(1, _possibleFirst.Count)]);
            elements.Add(_possibleSecond[Random.Range(0, _possibleSecond.Count)]);
            elements.Add(_possibleThird[Random.Range(0, _possibleThird.Count)]);
            _correct = elements;
        }

        private IEnumerator WinSequence(float f)
        {
            _correctGraphic.gameObject.SetActive(true);
            yield return new WaitForSeconds(f);
            SetProgress(1.0f);
        }
    }
}