using System;
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
        [SerializeField] private List<IdentityElement> _possibleFourth;

        private List<IdentityElement> _currentlySelected;
        private List<IdentityElement> _correct;

        protected override void Start()
        {
            base.Start();
            GenerateCorrect();
            _target.Load(_correct);
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
            SetProgress(correct);
        }

        private void GenerateCorrect()
        {
            List<IdentityElement> elements = new List<IdentityElement>();
            elements.Add(_possibleFirst[Random.Range(1, _possibleFirst.Count)]);
            elements.Add(_possibleSecond[Random.Range(0, _possibleSecond.Count)]);
            elements.Add(_possibleThird[Random.Range(0, _possibleThird.Count)]);
            elements.Add(_possibleFourth[Random.Range(0, _possibleFourth.Count)]);
            _correct = elements;
        }
    }
}