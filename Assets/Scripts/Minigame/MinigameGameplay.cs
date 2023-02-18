using UnityEngine;

namespace Minigame
{
    public abstract class MinigameGameplay : MonoBehaviour
    {
        private Minigame _minigame;

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