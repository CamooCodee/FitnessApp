using System;
using UnityEngine;

namespace FitnessApp.UICore.MovableElementList
{
    public class MovableElementList : MonoBehaviour
    {
        [SerializeField] private UIRebuilder rebuilder;
        [SerializeField] private RectTransform toRebuild;
        [SerializeField] private GameObject placeholderPrefab;
        private GameObject _placeholder;
        private bool _isMovingElement;
        protected IMovableElement currentElement;

        private void Awake()
        {
            Setup();
        }

        protected virtual void Setup()
        {
            rebuilder.Require(this);
            toRebuild.Require(this);
            placeholderPrefab.Require(this);
        }

        private void Update()
        {
            if (!_isMovingElement) return;
            OnMove();
        }

        protected virtual void OnMove()
        {
            SetPlaceholderByYPos(currentElement.YPos);
        }
        
        public void StartMovingElement(IMovableElement element)
        {
            if (_isMovingElement)
            {
                Debug.LogWarning("Cannot move two elements at once.");
                return;
            }

            _isMovingElement = true;

            currentElement = element ?? throw new ArgumentException("Cannot start moving null element.", nameof(element));
            
            _placeholder = Instantiate(placeholderPrefab, transform);
            Rebuild();
        }
        
        /// <returns>Returns the latest index of the placeholder</returns>
        public int StopMovingCurrentElement(int defaultReturn = 0)
        {
            if (!_isMovingElement)
            {
                Debug.LogWarning("No element started moving. Stop moving call was ignored.");
                return defaultReturn;
            }
            _isMovingElement = false;

            currentElement = null;
            
            int placeholderSiblingIndex = _placeholder.transform.GetSiblingIndex();
            Destroy(_placeholder);

            Rebuild();
            return placeholderSiblingIndex;
        }

        private void SetPlaceholderByYPos(float yPos)
        {
            if (!_isMovingElement)
            {
                Debug.LogWarning("Can't set placeholder when no element is moving. Did you forget to call StartMovingElement?");
                return;                    
            }
            
            var closestSiblingIndex = FindClosestSiblingIndexTo(yPos);
            _placeholder.transform.SetSiblingIndex(closestSiblingIndex);
        }

        protected virtual int FindClosestSiblingIndexTo(float yPos)
        {
            float minDistance = float.MaxValue;
            int closestSibling = 0;
            
            for (int i = 0; i < transform.childCount; i++)
            {
                var childYPos = GetChildYPos(i);
                
                var childDistance = Mathf.Abs(yPos - childYPos);
                if (childDistance > minDistance) continue;
                
                minDistance = childDistance;
                closestSibling = i;
            }

            return closestSibling;
        }

        protected float GetChildYPos(int siblingIndex)
        {
            return transform.GetChild(siblingIndex).position.y;
        }
        
        void Rebuild()
        {
            rebuilder.RebuildUILayout(toRebuild);
        }
    }
}