using System;
using TMPro;
using UnityEngine;

namespace FitnessApp.UICore.MovableElementList
{
    public class MovableElement : MonoBehaviour, IMovableElement
    {
        private bool _isDragged;
        private IDragInput _input;
        private RectTransform _rectTransform;
        [SerializeField] private Transform onDragParent;
        private Transform _originalParent; 
        [SerializeField] private int dragParentDistance = 1;
        private bool _handleParents;
        
        private MovableElementList _list;

        #region Unity Events And Setup

        private void OnValidate()
        {
            dragParentDistance = Mathf.Max(dragParentDistance, 1);
        }

        #region Setup

        private void Awake()
        {
            _debugText = GameObject.Find("TouchDebugDisplay")?.GetComponent<TextMeshProUGUI>();
            if(_debugText != null) _debugText.text = "Found: Movable";
            GetRequiredComponents();
            SetupParentHandling();
        }

        void GetRequiredComponents()
        {
            _list = GetComponentInParent<MovableElementList>();
            _list.Require(this);
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.Require(this);
            _input = GetComponent<IDragInput>();
            _input.Require(this);
        }

        void SetupParentHandling()
        {
            _originalParent = transform.parent;
            
            if (onDragParent != null)
                _handleParents = true;
            else if (_originalParent == null)
                _handleParents = false;
            else
            {
                SetOnDragParent();
                _handleParents = true;
            }
        }

        void SetOnDragParent()
        {
            var parent = transform;
            for (int i = 0; i < dragParentDistance; i++)
            {
                parent = parent.parent;
                if (parent == null)
                {
                    if(_debugText != null)_debugText.text = "Exception";
                    throw new ArgumentOutOfRangeException($"{nameof(dragParentDistance)}");
                }
            }

            onDragParent = parent;
            if(_debugText != null)_debugText.text = parent.name;
        }
        
        #endregion

        private void Update()
        {
            if(!_isDragged || !_input.CanGetInput) return;
            OnDrag();
        }

        #endregion

        #region Main Response

        public void OnBeginDrag()
        {
            if (_handleParents) transform.SetParent(onDragParent);
            
            _list.StartMovingElement(this);
            _isDragged = true;
        }
        private TextMeshProUGUI _debugText;
        void OnDrag()
        {
            var receivedPos = _input.GetElementTargetPosition(_rectTransform);
            transform.position = receivedPos;
        }
        
        public void OnEndDrag()
        {
            if (_handleParents) transform.SetParent(_originalParent);

            var newSiblingIndex = _list.StopMovingCurrentElement();
            transform.SetSiblingIndex(newSiblingIndex);

            _isDragged = false;
        }

        #endregion
        
        public float YPos => transform.position.y;
    }
}