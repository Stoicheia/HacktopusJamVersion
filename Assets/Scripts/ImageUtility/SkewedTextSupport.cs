using System;
using UnityEngine;

namespace ImageUtility
{
    [RequireComponent(typeof(SkewedText))]
    [ExecuteAlways]
    public class SkewedTextSupport : MonoBehaviour
    {
        public float SkewX;
        public float SkewY;

        private SkewedText _text;

        private void Update()
        {
            if (_text == null) _text = GetComponent<SkewedText>();
            _text.skewX = SkewX;
            _text.skewY = SkewY;
        }
    }
}