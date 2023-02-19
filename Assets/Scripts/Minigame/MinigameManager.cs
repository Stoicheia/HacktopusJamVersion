using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Minigame
{
    public class MinigameManager : MonoBehaviour
    {
        [SerializeField] private List<GameLayout> _layouts;
        [SerializeField] private List<Minigame> _gamePrefabs;
        [SerializeField] private Camera _camera;

        private Dictionary<Minigame, bool> _gameCompleted;
        private Dictionary<Minigame, bool> _gameLoaded;
        private Dictionary<Minigame, GameLayout> _gameToLayout;
        public Dictionary<Minigame, bool> GameCompleted => _gameCompleted;
        public int MinigamesCompleted => GameCompleted.Count(x => x.Value);
        public int MinigamesCount => _gameCompleted.Count;

        private float _timer;
        private bool _isPlaying;

        public float Timer => _timer;

        private void OnEnable()
        {
            _gameCompleted = new Dictionary<Minigame, bool>();
            _gameLoaded = new Dictionary<Minigame, bool>();
            _gameToLayout = new Dictionary<Minigame, GameLayout>();
            foreach (var g in _gamePrefabs)
            {
                _gameCompleted[g] = false;
                _gameLoaded[g] = false;
            }

            foreach (var l in _layouts)
            {
                l.IsFree = true;
            }
            
            Minigame.OnComplete += HandleComplete;
            Minigame.OnFail += HandleFail;
            GameLayout.OnRequestLoad += TryLoadRandomGame;
        }

        private void Start()
        {
            Initialise();
        }

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

        private void Update()
        {
            if (_isPlaying) _timer += Time.deltaTime;
        }

        public void TryLoadRandomGame(GameLayout layout)
        {
            Minigame unfinishedGame = GetRandomUnfinishedGame();
            if (unfinishedGame == null)
            {
                Debug.LogWarning("You finished! No more to load...");
                return;
            }
            if (!layout.IsFree)
            {
                Debug.LogWarning("NONONONO!!");
                return;
            }
            LoadGame(unfinishedGame, layout);
        }

        public void LoadRandomGame()
        {
            Minigame unfinishedGame = GetRandomUnfinishedGame();
            if(unfinishedGame == null) Debug.Log("All done! No more games to load.");
            GameLayout layout;
            List<GameLayout> freeLayouts = _layouts.Where(x => x.IsFree).ToList();
            if(freeLayouts.Count == 0) Debug.Log("No no! You return aomori!");
            layout = freeLayouts[Random.Range(0, freeLayouts.Count)];
            LoadGame(unfinishedGame, layout);
        }

        private void LoadGame(Minigame game, GameLayout gl)
        {
            Minigame gameInstance = Instantiate(game);
            gameInstance.SetCamera(_camera);
            gameInstance.SetBounds(gl);
            gl.IsFree = false;
            gameInstance.Prefab = game;
            _gameToLayout[gameInstance] = gl;
            _gameLoaded[game] = true;
        }

        private void UnloadGame(Minigame gameInstance)
        {
            _gameToLayout[gameInstance].IsFree = true;
            _gameToLayout.Remove(gameInstance);
            Destroy(gameInstance.gameObject);
            _gameLoaded[gameInstance.Prefab] = false;
        }

        private Minigame GetRandomUnfinishedGame()
        {
            List<Minigame> unfinished = _gameCompleted.Where(x => !x.Value && !_gameLoaded[x.Key])
                .Select(x => x.Key).ToList();
            if (unfinished.Count == 0) return null;
            int rng = Random.Range(0, unfinished.Count);
            return unfinished[rng];
        }

        private void HandleComplete(Minigame instance)
        {
            _gameCompleted[instance.Prefab] = true;
            UnloadGame(instance);
        }

        private void HandleFail(Minigame instance)
        {
            UnloadGame(instance);
        }
    }
}