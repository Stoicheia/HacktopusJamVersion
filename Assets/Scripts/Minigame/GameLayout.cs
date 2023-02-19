using System;
using Animations;
using Minigame.Games.Core;
using UnityEngine;

namespace Minigame
{
    public class GameLayout : MonoBehaviour
    {
        public static event Action<GameLayout> OnRequestLoad;
        private bool _isFree;
        public bool IsFree
        {
            get => _isFree;
            set
            {
                _isFree = value;
                _backScreen.gameObject.SetActive(_isFree);
            }
        }

        [SerializeField] public float UniversalScale;
        [SerializeField] public SkewedImage UI;
        [SerializeField] public Transform Game;
        [SerializeField] public SkewedImage Progress;
        [SerializeField] public SkewedImage InstructionPanel;
        [SerializeField] private SkewedImage _backScreen;

        [SerializeField] private MinigameTransitionScreen _transitionIn;
        [SerializeField] private MinigameTransitionScreen _transitionOutWin;
        [SerializeField] private MinigameTransitionScreen _transitionOutFail;

        [SerializeField] private KeyCode _loadKey;

        private InputPoller _inputs;

        private void Start()
        {
            _inputs = FindObjectOfType<InputPoller>();
        }

        private void Update()
        {
            if (_inputs.GetKeyDown(_loadKey) && IsFree)
            {
                TransitionIn();
            }
        }

        private void OnEnable()
        {
            _transitionIn.OnEnd += LoadGame;
            _transitionOutFail.OnEnd += Free;
            _transitionOutWin.OnEnd += Free;
            
            _transitionIn.gameObject.SetActive(false);
            _transitionOutFail.gameObject.SetActive(false);
            _transitionOutWin.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _transitionIn.OnEnd -= LoadGame;
            _transitionOutFail.OnEnd -= Free;
            _transitionOutWin.OnEnd -= Free;
        }

        public void TransitionIn()
        {
            _transitionIn.Play();
            IsFree = false;
        }

        public void TransitionOutFail()
        {
            _transitionOutFail.Play();
        }

        public void TransitionOutWin()
        {
            _transitionOutWin.Play();
        }

        private void Free()
        {
            IsFree = true;
        }

        private void Unfree()
        {
            IsFree = false;
        }

        private void LoadGame()
        {
            OnRequestLoad?.Invoke(this);
        }
    }
}