using System;
using UnityEngine;

namespace Minigame.Games
{
    public class Hurdles : MinigameGameplay
    {
        [Header("Controls")] [SerializeField] private KeyCode _jumpKey;
        [Header("Config")] [SerializeField] private float _speed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _gravity;
        [Header("Transforms")]
        [SerializeField] private HurdlesPlayer _player;
        [SerializeField] private RectTransform _left;
        [SerializeField] private RectTransform _right;

        private RectTransform _playerT;

        private void OnEnable()
        {
            _player.OnDie += HandleDie;
        }

        private void OnDisable()
        {
            _player.OnDie -= HandleDie;
        }

        private void Start()
        {
            _playerT = _player.GetComponent<RectTransform>();
        }

        private void Update()
        {
            SetProgress(Mathf.InverseLerp(_left.rect.x, _right.rect.x, _playerT.rect.x));
        }

        private void HandleDie()
        {
            Fail();
        }
    }
}