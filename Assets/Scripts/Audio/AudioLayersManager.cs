using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigame.Games.Audio
{

    public class AudioLayersManager : MonoBehaviour
    {
        [SerializeField] private List<AudioLayerPlayer> _layers;
        [SerializeField] private AudioLayerPlayer _specialLayer;

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
            MinigameManager.OnLoadSpecial += HandleLoadSpecial;
            MinigameManager.OnUnloadSpecial += HandleUnloadSpecial;
        }

        private void OnDisable()
        {
            MinigameManager.OnComplete -= HandleMinigameWin;
            MinigameManager.OnLoadSpecial -= HandleLoadSpecial;
            MinigameManager.OnUnloadSpecial -= HandleUnloadSpecial;
        }

        public void Initialise()
        {
            foreach (var player in _layers)
            {
                player.Play(player.Id < 0);
                player.VolumeMultiplier = 1;
                _specialLayer.VolumeMultiplier = 0;
            }
            _specialLayer.Play(false);
            StartCoroutine(ResyncAfter3());
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

        private void HandleLoadSpecial()
        {
            foreach (var player in _layers)
            {
                player.VolumeMultiplier = 0;
            }

            _specialLayer.VolumeMultiplier = 1;
            _specialLayer.Unmute();
        }

        private void HandleUnloadSpecial()
        {
            foreach (var player in _layers)
            {
                player.VolumeMultiplier = 1;
            }

            _specialLayer.VolumeMultiplier = 0;
            _specialLayer.Mute();
        }

        private IEnumerator ResyncAfter3()
        {
            yield return new WaitForSeconds(3);
            Resync();
        }

        private void Resync()
        {
            foreach (var player in _layers)
            {
                player.SetTime(3);
            }
        }
    }
}