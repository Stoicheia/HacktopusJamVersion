using System;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Games.Audio
{

    public class AudioLayersManager : MonoBehaviour
    {
        [SerializeField] private MinigameManager _game;
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
    }
}