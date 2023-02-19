using System;
using UnityEngine;

namespace ImageUtility
{
    //Place on container
    public class SkewPosition : MonoBehaviour
    {
        public RectTransform Parent { get; set; }
        [SerializeField] private RectTransform _graphics;

        private void Update()
        {
            //_graphics.position = 
        }
    }
}