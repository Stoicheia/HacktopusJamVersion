using System;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Games.Audio
{

    public class AudioLayersManager : MonoBehaviour
    {
        [SerializeField] private List<AudioLayerPlayer> _layers;

        private Dictionary<int, AudioLayerPlayer> _idToLayer;

        private void Awake()
        {
            _idToLayer = new Dictionary<int, AudioLayerPlayer>();
            foreach (var l in _layers)
            {
                _idToLayer[l.Id] = l;
            }
            
        }

        private void OnEnable()
        {
            MinigameManager.OnComplete += HandleMinigameWin;
        }

        private void OnDisable()
        {
            MinigameManager.OnComplete -= HandleMinigameWin;
        }

        public void Initialise()
        {
            foreach (var player in _layers)
            {
                player.Play(player.Id < 0);
            }
        }

        private void HandleMinigameWin(Minigame game)
        {
            int gameId = game.TypeID;
            if (!_idToLayer.ContainsKey(gameId))
            {
                Debug.LogWarning($"No music associated with game id {gameId}");
                return;
            }
            _idToLayer[gameId].Unmute();
        }
    }
}