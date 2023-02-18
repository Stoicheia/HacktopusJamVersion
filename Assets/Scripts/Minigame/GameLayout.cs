using System;
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
        [SerializeField] public SkewedImage UI;
        [SerializeField] public Transform Game;
        [SerializeField] public SkewedImage Progress;
        [SerializeField] public SkewedImage InstructionPanel;
        [SerializeField] private SkewedImage _backScreen;

        [SerializeField] private KeyCode _loadKey;

        private void Update()
        {
            if (Input.GetKeyDown(_loadKey))
            {
                OnRequestLoad?.Invoke(this);
            }
        }
    }
}