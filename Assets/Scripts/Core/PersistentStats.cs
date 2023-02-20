using System;
using UnityEngine;

namespace Minigame.Games.Core
{
    public class PersistentStats : MonoBehaviour
    {
        private static PersistentStats _instance;
        public float BestTime { get; set; }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            BestTime = Single.PositiveInfinity;
        }
    }
}