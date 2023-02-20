using System;
using Animations;
using Unity.VisualScripting;
using UnityEngine;

namespace Minigame.Games.Core
{
    [RequireComponent(typeof(AudioSource))]
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private ClickFlash _clickFlash;
        [SerializeField] private AudioClip _flashSfx;
        private void Update()
        {
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
            {
                GetComponent<AudioSource>().PlayOneShot(_flashSfx);
                _clickFlash.Enter();
            }
        }
    }
}