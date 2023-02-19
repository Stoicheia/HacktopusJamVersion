using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Games.Identity
{
    public class IdentityOption : MonoBehaviour
    {
        [field: SerializeField] public KeyCode Key { get; private set; }
        [SerializeField] private List<IdentityElement> _elements;
        [SerializeField] private Image _picture;
        private int _ptr;
        private IdentityElement _selectedElement;

        public IdentityElement SelectedElement => _selectedElement;

        private void Start()
        {
            _ptr = -1;
            Toggle();
        }

        public void Toggle()
        {
            _ptr++;
            _selectedElement = _elements[_ptr % _elements.Count];
            _picture.sprite = _selectedElement._sprite;
        }
    }
}