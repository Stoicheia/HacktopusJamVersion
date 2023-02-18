using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Minigame
{
    public class MinigameManager : MonoBehaviour
    {
        [SerializeField] private List<GameLayout> _layouts;
        [SerializeField] private List<Minigame> _gamePrefabs;
        [SerializeField] private Camera _camera;

        private Dictionary<Minigame, bool> _gameCompleted;
        public Dictionary<Minigame, bool> GameCompleted => _gameCompleted;
        public int MinigamesCompleted => GameCompleted.Count(x => x.Value);
        public int MinigamesCount => _gameCompleted.Count;
        
        private float _timer;
        private bool _isPlaying;
        
        public void Initialise()
        {
            _timer = 0;
            _isPlaying = true;
        }

        public float Stop()
        {
            _isPlaying = false;
            return _timer;
        }

        private void OnEnable()
        {
            foreach (var g in _gamePrefabs)
            {
                _gameCompleted[g] = false;
                g.OnComplete += () =>
                {
                    _gameCompleted[g] = true;
                };
                g.OnFail += () =>
                {
                    Penalise(g);
                };
            }
        }

        private void Update()
        {
            if (_isPlaying) _timer += Time.deltaTime;
        }
        
        private void LoadGame(Minigame game, GameLayout gl)
        {
            Minigame gameInstance = Instantiate(game);
            gameInstance.SetBounds(gl);
            gameInstance.SetCamera(_camera);
        }

        public void Penalise(Minigame m)
        {
            
        }
        
    }
}