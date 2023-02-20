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
            foreach (var player in _layers)
            {
                if(player.Id < 0) player.Unmute();
                else player.Mute();
            }
        }

        private void OnEnable()
        {
            MinigameManager.OnLoad += HandleMinigameLoad;
            MinigameManager.OnUnload += HandleMinigameUnload;
        }

        private void OnDisable()
        {
            MinigameManager.OnLoad -= HandleMinigameLoad;
            MinigameManager.OnUnload -= HandleMinigameUnload;
        }

        private void Start()
        {
            
        }

        private void HandleMinigameLoad(Minigame game)
        {
            int gameId = game.TypeID;
            if (!_idToLayer.ContainsKey(gameId))
            {
                Debug.LogWarning($"No music associated with game id {gameId}");
                return;
            }
            _idToLayer[gameId].Unmute();
        }

        private void HandleMinigameUnload(Minigame game)
        {
            int gameId = game.TypeID;
            if (!_idToLayer.ContainsKey(gameId))
            {
                Debug.LogWarning($"No music associated with game id {gameId}");
                return;
            }
            _idToLayer[gameId].Mute();
        }
    }
}