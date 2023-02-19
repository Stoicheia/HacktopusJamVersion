using System;
using Animations;
using Unity.VisualScripting;
using UnityEngine;

namespace Minigame.Games.Core
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private ClickFlash _clickFlash;
        private void Update()
        {
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
            {
                _clickFlash.Enter();
            }
        }
    }
}