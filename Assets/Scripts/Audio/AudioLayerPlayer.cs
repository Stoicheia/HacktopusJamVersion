using System;
using UnityEngine;

namespace Minigame.Games.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioLayerPlayer : MonoBehaviour
    {
        private AudioSource _audio;
        private float _targetVolume;
        [SerializeField] [Range(0.1f, 10)] private float _fadeStrength;
        [SerializeField] private int _id;

        public int Id => _id;

        public float Volume
        {
            get => _audio.volume;
            set => _targetVolume = value;
        }

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }

        private void Update()
        {
            _audio.volume = Mathf.Lerp(_audio.volume, _targetVolume, _fadeStrength * Time.deltaTime);
        }

        public void Play(bool withVolume)
        {
            _audio.Play();
            if(withVolume) Unmute(true);
            else Mute(true);
        }

        public void Mute(bool force = false)
        {
            Volume = 0;
            if (force) _audio.volume = 0;
        }

        public void Unmute(bool force = false)
        {
            Volume = 1;
            if (force) _audio.volume = 1;
        }
}
}