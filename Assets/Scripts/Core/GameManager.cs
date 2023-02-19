using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minigame.Games.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<KeyCode> _pauseButtons;
        [SerializeField] private InputPoller _inputs;
        [SerializeField] private RectTransform _pauseMenu;

        private bool _paused;

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
    }
}