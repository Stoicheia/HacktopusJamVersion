using System;
using System.Collections;
using System.Collections.Generic;
using Animations;
using DG.Tweening;
using LootLocker.Requests;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Minigame.Games.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private MinigameManager _minigames;
        [SerializeField] private List<KeyCode> _pauseButtons;
        [SerializeField] private InputPoller _inputs;
        [SerializeField] private RectTransform _pauseMenu;
        [SerializeField] private EndScreen _endScreen;
        [SerializeField] private RectTransform _startScreen;
        [SerializeField] private Animator _countdown;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Image _activeLeaderboardImage;
        [SerializeField] private Image _inactiveLeaderboardImage;
        [SerializeField] private MonitorFlyby _flyby;

        private bool _paused;
        private bool _finished;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public float BestTime { get; set; }

        private IEnumerator Start()
        {
            _leaderboardButton.interactable = false;
            _inactiveLeaderboardImage.gameObject.SetActive(true);
            _activeLeaderboardImage.gameObject.SetActive(false);
            
            _finished = false;
            BestTime = Single.PositiveInfinity;
            _inputs.InputsEnabled = false;
            _startScreen.gameObject.SetActive(true);
            
            bool done = false;
            LootLockerSDKManager.StartGuestSession((response) => {
                if (response.success) {
                    PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                    _leaderboardButton.interactable = true;
                    _inactiveLeaderboardImage.gameObject.SetActive(false);
                    _activeLeaderboardImage.gameObject.SetActive(true);

                    done = true;
                } else {
                    done = true;
                }
            });
            yield return new WaitWhile(() => done == false);
        }

        private void Update()
        {
            foreach (var k in _pauseButtons)
            {
                if (Input.GetKeyDown(k))
                {
                    if (!_paused)
                    {
                        _pauseMenu.gameObject.SetActive(true);
                        _paused = true;
                        _inputs.InputsEnabled = false;
                        Time.timeScale = 0;
                    }
                    else
                    {
                        _pauseMenu.gameObject.SetActive(false);
                        _paused = false;
                        _inputs.InputsEnabled = true;
                        Time.timeScale = 1;
                    }

                    break;
                }
            }

            if (_minigames.Finished && !_finished)
            {
                End();
                _finished = true;
            }
        }

        public void Restart()
        {
            _pauseMenu.gameObject.SetActive(false);
            _paused = false;
            _inputs.InputsEnabled = true;
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
            DOTween.Clear();
            DOTween.ClearCachedTweens();
        }

        public void BeginGameSequence()
        {
            StartCoroutine(StartGameSequence());
            _startScreen.gameObject.SetActive(false);
        }

        private void BeginGame()
        {
            _minigames.Initialise();
            _inputs.InputsEnabled = true;
        }

        public void End()
        {
            _flyby.Stop();
            _minigames.Stop();
            
            _endScreen.gameObject.SetActive(true);
            _paused = true;
            _inputs.InputsEnabled = false;

            float finalTime = _minigames.Timer;
            _endScreen.FinalTime = finalTime;
            if (finalTime < BestTime)
            {
                BestTime = finalTime;
            }
            _endScreen.BestFinalTime = BestTime;
        }

        private IEnumerator StartGameSequence()
        {
            _countdown.gameObject.SetActive(true);
            _countdown.Play("countdown");
            yield return new WaitForSeconds(3.5f);
            _countdown.gameObject.SetActive(false);
            BeginGame();
            _flyby.Begin();
        }
    }
}