using System;
using UnityEngine;

namespace Minigame.Games.Core
{
    public class InputPoller : MonoBehaviour
    {
        public bool InputsEnabled { get; set; }
        public Minigame InputOwner = null;

        private void Start()
        {
            //InputsEnabled = true;
        }

        public bool GetKeyDown(KeyCode k)
        {
            return (InputsEnabled && Input.GetKeyDown(k));
        }
        
        public bool GetKeyDownLocked(KeyCode k)
        {
            return (InputsEnabled && Input.GetKeyDown(k) && InputOwner == null);
        }

        public bool GetKeyDown(KeyCode k, Minigame m)
        {
            return (InputsEnabled && Input.GetKeyDown(k) 
                                  && (InputOwner == null || m == null || InputOwner == m.Prefab || InputOwner.Prefab == m));
        }
    }
}