using System;
using Minigame.Games.Core;
using UnityEngine;

namespace Minigame
{
    public abstract class MinigameGameplay : MonoBehaviour
    {
        private Minigame _minigame;
        protected float _scale => _minigame.Scale;

        protected InputPoller _inputs;

        protected virtual void Start()
        {
            _inputs = FindObjectOfType<InputPoller>();
        }

        public void SetGame(Minigame g)
        {
            _minigame = g;
        }

        protected void SetProgress(float f)
        {
            _minigame.Progress = f;
        }

        protected void Fail()
        {
            _minigame.InvokeFail();
        }
    }
}