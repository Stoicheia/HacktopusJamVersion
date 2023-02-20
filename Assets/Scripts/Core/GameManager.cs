using System;
using System.Collections;
using System.Collections.Generic;
using Animations;
using DG.Tweening;
using LootLocker.Requests;
using Minigame.Games.Audio;
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
        [SerializeField] private KeyCode _restartButton;
        [SerializeField] private InputPoller _inputs;
        [SerializeField] private RectTransform _pauseMenu;
        [SerializeField] private EndScreen _endScreen;
        [SerializeField] private RectTransform _startScreen;
        [SerializeField] private Animator _countdown;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Image _activeLeaderboardImage;
        [SerializeField] private Image _inactiveLeaderboardImage;
        [SerializeField] private MonitorFlyby _flyby;

        [SerializeField] private AudioLayersManager _audioLayers;
        [SerializeField] private PersistentStats _persistent;
        [SerializeField] private AudioSource _audioSettings;
        [SerializeField] private AudioSource _audioCountdown;

        private bool _paused;
        private bool _finished;

        private bool _started;

        private void Awake()
        {
            //DontDestroyOnLoad(gameObject);
        }

        private IEnumerator Start()
        {
            _started = false;
            _leaderboardButton.interactable = false;
            _inactiveLeaderboardImage.gameObject.SetActive(true);
            _activeLeaderboardImage.gameObject.SetActive(false);
            
            _finished = false;
            _inputs.InputsEnabled = false;
            _startScreen.gameObject.SetActive(true);
            
            bool done = false;

            _audioLayers.Initialise();
            yield return new WaitForSeconds(0.1f);

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
            if (!_started && Input.GetKeyDown(KeyCode.Return))
            {
                BeginGameSequence();
            }
            if (_inputs.GetKeyDownLockedPure(_restartButton))
            {
                Restart();
            }
            foreach (var k in _pauseButtons)
            {
                if (_inputs.GetKeyDownLockedPure(k))
                {
                    if (!_paused)
                    {
                        _audioSettings.PlayOneShot(_audioSettings.clip);
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
            if(_audioCountdown != null)
                _audioCountdown.Play();
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
            if (finalTime < _persistent.BestTime)
            {
                _persistent.BestTime = finalTime;
            }
            _endScreen.BestFinalTime = _persistent.BestTime;
        }

        private IEnumerator StartGameSequence()
        {
            _started = true;
            _countdown.gameObject.SetActive(true);
            _countdown.Play("countdown");
            _flyby.Begin();
            yield return new WaitForSeconds(3f);
            BeginGame();
            yield return new WaitForSeconds(0.5f);
            _countdown.gameObject.SetActive(false);
            BeginGame();
        }
    }
}