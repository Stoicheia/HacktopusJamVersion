using System;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Games.Audio
{
    public enum MusicLayerName
    {
        
    }
    
    public class AudioLayersManager : MonoBehaviour
    {
        [SerializeField] private MinigameManager _game;
        [SerializeField] private List<AudioLayerPlayer> _layers;

        private Dictionary<MusicLayerName, AudioLayerPlayer> _nameToLayer;

        private void Awake()
        {
            _nameToLayer = new Dictionary<MusicLayerName, AudioLayerPlayer>();
            foreach (var l in _layers)
            {
                _nameToLayer[l.Name] = l;
            }
        }
    }
}