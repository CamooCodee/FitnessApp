using System;
using UnityEngine;

namespace UIConcretes.Elements.Manager
{
    public class DropdownElementList : MonoBehaviour
    {
        private DropdownElementMono[] _elements;
        private DropdownElementMono _currentlyExpanded = null;
        
        private void Awake()
        {
            Setup();
        }

        public void Setup()
        {
            FindDropdownElements();
            SetupDropdownElements();
        }
        
        private void FindDropdownElements()
        {
            _elements = GetComponentsInChildren<DropdownElementMono>();
            if(_elements == null) _elements = new DropdownElementMono[0];
        }

        private void SetupDropdownElements()
        {
            for (var i = 0; i < _elements.Length; i++)
            {
                var iCopy = i;
                _elements[i].ListenForOnExpand(delegate { OnDropdownExpand(_elements[iCopy]); });
            }
        }

        void OnDropdownExpand(DropdownElementMono element)
        {
            if(_currentlyExpanded != null && _currentlyExpanded != element) _currentlyExpanded.HideDropdown();
            _currentlyExpanded = element;
        }
    }
}