using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Minigame.Games.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Minigame
{
    [Serializable]
    public struct GameRoundPair
    {
        public Minigame Game;
        public int AfterRound;
    }
    public class MinigameManager : MonoBehaviour
    {
        public static Action<Minigame> OnLoad;
        public static Action<Minigame> OnUnload;
        public static Action<Minigame> OnComplete;
        
        public static Action OnLoadSpecial;
        public static Action OnUnloadSpecial;
        
        [SerializeField] private List<GameLayout> _layouts;
        [SerializeField] private List<Minigame> _gamePrefabs;
        [SerializeField] private List<GameRoundPair> _forcedGamePrefabs;
        [SerializeField] private GameLayout _forcingLayout;
        [SerializeField] private Camera _camera;
        [SerializeField] private AudioSource _audioSuccess;
        [SerializeField] private AudioSource _audioFail;
        [SerializeField] private RectTransform _specialCover;


        private Dictionary<Minigame, bool> _gameCompleted;
        private Dictionary<Minigame, bool> _gameCompletedNormal;
        private Dictionary<Minigame, bool> _gameLoaded;
        private List<Minigame> _specialGames;
        private Dictionary<Minigame, GameLayout> _gameToLayout;
        public Dictionary<Minigame, bool> GameCompleted => _gameCompleted;
        public int MinigamesCompleted => GameCompleted.Count(x => x.Value);
        public int MinigamesCount => _gameCompleted.Count;

        private float _timer;
        private bool _isPlaying;

        private InputPoller _inputs;

        public float Timer => _timer;

        public bool Finished => MinigamesCompleted == MinigamesCount;

        private void OnEnable()
        {
            _specialCover.gameObject.SetActive(false);
            _gameCompleted = new Dictionary<Minigame, bool>();
            _gameLoaded = new Dictionary<Minigame, bool>();
            _gameToLayout = new Dictionary<Minigame, GameLayout>();
            _specialGames = new List<Minigame>();
            foreach (var g in _gamePrefabs)
            {
                _gameCompleted[g] = false;
                _gameLoaded[g] = false;
            }
            foreach (var g in _forcedGamePrefabs)
            {
                _gameCompleted[g.Game] = false;
                _gameLoaded[g.Game] = false;
                _specialGames.Add(g.Game);
            }

            foreach (var l in _layouts)
            {
                l.IsFree = true;
            }
            
            Minigame.OnComplete += HandleComplete;
            Minigame.OnFail += HandleFail;
            GameLayout.OnRequestLoad += TryLoadRandomGame;
            GameLayout.OnRequestLoadRequested += TryLoadRequested;
        }
        
        private void OnDisable()
        {
            Minigame.OnComplete -= HandleComplete;
            Minigame.OnFail -= HandleFail;
            GameLayout.OnRequestLoad -= TryLoadRandomGame;
            GameLayout.OnRequestLoadRequested -= TryLoadRequested;
        }

        private void Start()
        {
            _inputs = FindObjectOfType<InputPoller>();
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
            foreach (var g in _forcedGamePrefabs)
            {
                var roundToSpawn = g.AfterRound;
                var game = g.Game;

                if ((!_gameCompleted[game] && !_gameLoaded[game]) && MinigamesCompleted > roundToSpawn)
                {
                    ForceLoadGame(_forcingLayout, game);
                }
            }
        }

        private void TryLoadRequested(GameLayout gameLayout, Minigame minigame)
        {
            if(minigame != null)
                LoadGame(minigame, gameLayout);
        }

        public void ForceLoadGame(GameLayout layout, Minigame game)
        {
            StartCoroutine(ForceLoadGameSequence(layout, game));
        }

        private IEnumerator ForceLoadGameSequence(GameLayout layout, Minigame game)
        {
            yield return new WaitWhile(() => !layout.IsFree && !layout.PermaFreeze);
            if (!_gameCompleted[game])
            {
                if (_specialGames.Contains(game))
                {
                    _inputs.InputOwner = game;
                    _specialCover.gameObject.SetActive(true);
                    OnLoadSpecial?.Invoke();
                }
                _forcingLayout.TransitionInSpecial(game);
            }
        }

        public void TryLoadRandomGame(GameLayout layout)
        {
            Minigame unfinishedGame;
            bool represented;
            int tries = 5;
            do
            {
                unfinishedGame = GetRandomUnfinishedGame();
                represented = false;
                foreach (var g in _gameLoaded.Where(x => x.Value))
                {
                    if (unfinishedGame != null && unfinishedGame.TypeID.Equals(g.Key.TypeID)) represented = true;
                }
            } while (tries-- > 0 && represented);

            if (unfinishedGame == null)
            {
                Debug.LogWarning("You finished! No more to load...");
                layout.IsFree = false;
                layout.PermaFreeze = true;
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
            OnLoad?.Invoke(game);
        }

        private void UnloadGame(Minigame gameInstance, bool won)
        {
            if (_specialGames.Contains(gameInstance.Prefab))
            {
                _inputs.InputOwner = null;
                _specialCover.gameObject.SetActive(false);
                OnUnloadSpecial?.Invoke();
            }
            var gl = _gameToLayout[gameInstance];
            if (won)
            {
                gl.TransitionOutWin();
                OnComplete?.Invoke(gameInstance.Prefab);
            }
            else
            {
                gl.TransitionOutFail();
            }
            _gameToLayout.Remove(gameInstance);
            Destroy(gameInstance.gameObject);
            _gameLoaded[gameInstance.Prefab] = false;
            OnUnload?.Invoke(gameInstance.Prefab);
        }

        private Minigame GetRandomUnfinishedGame()
        {
            List<Minigame> unfinished = _gameCompleted.Where(x => !x.Value 
                                                                  && !_gameLoaded[x.Key] 
                                                                  && !_specialGames.Contains(x.Key))
                .Select(x => x.Key).ToList();
            if (unfinished.Count == 0) return null;
            int rng = Random.Range(0, unfinished.Count);
            return unfinished[rng];
        }

        private void HandleComplete(Minigame instance)
        {
            _gameCompleted[instance.Prefab] = true;
            UnloadGame(instance, true);
            _audioSuccess.PlayOneShot(_audioSuccess.clip);
        }

        private void HandleFail(Minigame instance)
        {
            UnloadGame(instance, false);
            _audioFail.PlayOneShot(_audioFail.clip);
        }
    }
}