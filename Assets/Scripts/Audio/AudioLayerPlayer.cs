using System;
using UnityEngine;

namespace Minigame.Games.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioLayerPlayer : MonoBehaviour
    {
        private AudioSource _audio;
        private float _targetVolume;
        [SerializeField] [Range(0.3f, 30)] private float _fadeStrength;
        [SerializeField] private MusicLayerName _name;

        public MusicLayerName Name => _name;

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
            if(withVolume) Unmute();
            else Mute();
        }

        public void Mute()
        {
            Volume = 0;
        }

        public void Unmute()
        {
            Volume = 1;
        }
}
}