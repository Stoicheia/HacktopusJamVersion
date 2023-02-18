using UnityEngine;

namespace Minigame
{
    public abstract class MinigameGameplay : MonoBehaviour
    {
        private Minigame _minigame;

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