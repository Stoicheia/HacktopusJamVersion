using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Minigame.Games
{
    public class SimonSays : MinigameGameplay
    {
        [SerializeField] private bool _good;
        [SerializeField] private float _time;
        private float _timer;
        [SerializeField] private List<KeyCode> _keySelection;
        [SerializeField] private TextMeshProUGUI _upTextField;
        [SerializeField] private TextMeshProUGUI _letterTextField;

        private List<KeyCode> _chosenKeys;

        protected override void Start()
        {
            base.Start();
            _good = Random.Range(0, 2) == 1;
            _timer = _good ? 1 : 0;
            _chosenKeys = new List<KeyCode>();
            _chosenKeys.Add(_keySelection[Random.Range(0, _keySelection.Count)]);
            if (_good)
            {
                _upTextField.text = "PRESS";
                _letterTextField.text = _chosenKeys[0].ToString().Last().ToString();
            }
            else
            {
                _upTextField.text = "DON'T PRESS";
                _letterTextField.text = _chosenKeys[0].ToString().Last().ToString();
            }
        }

        private void Update()
        {
            if (!_good)
            {
                foreach (var bk in _chosenKeys)
                {
                    if (_inputs.GetKeyDown(bk, _minigame)) Fail();
                }

                _timer += Time.deltaTime;
                SetProgress(_timer / _time);
            }
            else
            {
                foreach (var bk in _chosenKeys)
                {
                    if (_inputs.GetKeyDown(bk, _minigame)) SetProgress(1);
                }

                _timer -= Time.deltaTime;
                SetProgress(_timer/_time);
                if(_timer <= 0) Fail();
            }
        }
    }
}