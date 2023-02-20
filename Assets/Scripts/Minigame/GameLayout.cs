using System;
using Animations;
using Minigame.Games.Core;
using UnityEngine;

namespace Minigame
{
    public class GameLayout : MonoBehaviour
    {
        public static event Action<GameLayout> OnRequestLoad;
        public static event Action<GameLayout, Minigame> OnRequestLoadRequested;
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

        private bool _permaFreeze;
        public bool PermaFreeze
        {
            get => _permaFreeze;
            set
            {
                _permaFreeze = value;
                _freezeScreen.gameObject.SetActive(value);
            }
        }

        [SerializeField] public float UniversalScale;
        [SerializeField] public SkewedImage UI;
        [SerializeField] public Transform Game;
        [SerializeField] public SkewedImage Progress;
        [SerializeField] public SkewedImage InstructionPanel;
        [SerializeField] private SkewedImage _backScreen;

        [SerializeField] private MinigameTransitionScreen _transitionIn;
        [SerializeField] private MinigameTransitionScreen _transitionInSpecial;
        [SerializeField] private MinigameTransitionScreen _transitionOutWin;
        [SerializeField] private MinigameTransitionScreen _transitionOutFail;
        [SerializeField] private RectTransform _freezeScreen;

        [SerializeField] private KeyCode _loadKey;

        private InputPoller _inputs;

        private void Start()
        {
            _inputs = FindObjectOfType<InputPoller>();
            PermaFreeze = false;
        }

        private void Update()
        {
            if (_inputs.GetKeyDownLocked(_loadKey) && IsFree && !PermaFreeze)
            {
                TransitionIn();
            }
        }

        private void OnEnable()
        {
            _transitionIn.OnEnd += LoadGame;
            _transitionOutFail.OnEnd += Free;
            _transitionOutWin.OnEnd += Free;
            if(_transitionInSpecial != null) _transitionInSpecial.OnEndRequested += LoadGameRequested;
            _transitionIn.gameObject.SetActive(false);
            _transitionOutFail.gameObject.SetActive(false);
            _transitionOutWin.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _transitionIn.OnEnd -= LoadGame;
            _transitionOutFail.OnEnd -= Free;
            _transitionOutWin.OnEnd -= Free;
            if(_transitionInSpecial != null) _transitionInSpecial.OnEndRequested -= LoadGameRequested;
        }

        public void TransitionIn()
        {
            _transitionIn.Play();
            IsFree = false;
        }

        public void TransitionInSpecial(Minigame m)
        {
            _transitionInSpecial.Play(m);
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

        private void LoadGameRequested(Minigame m)
        {
            OnRequestLoadRequested?.Invoke(this, m);
        }
    }
}