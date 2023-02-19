using System;
using UnityEngine;

namespace Minigame.Games
{
    [RequireComponent(typeof(Collider2D))]
    public class HurdlesObstacle : MonoBehaviour
    {
        private Collider2D _col;

        private void Awake()
        {
            _col = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            HurdlesPlayer hp = col.gameObject.GetComponent<HurdlesPlayer>();
            if(hp != null) hp.Die();
        }
    }
}