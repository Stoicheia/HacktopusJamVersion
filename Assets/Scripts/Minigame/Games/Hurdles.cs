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
            _player.SetConfig(_speed, _jumpHeight, _gravity);
            _player.transform.position = _left.transform.position;
            _player.GroundY = _left.localPosition.y;
        }

        private void Update()
        {
            _player.GameScale = _scale;
            if (Input.GetKeyDown(_jumpKey))
            {
                _player.Jump();
            }
            SetProgress(Mathf.InverseLerp(_left.position.x, _right.position.x, _playerT.position.x));
        }

        private void HandleDie()
        {
            Fail();
        }
    }
}