using System;
using FitnessApp;
using FitnessApp.UICore;
using UnityEngine;

namespace UIConcretes.Elements
{
    public abstract class DropdownElementMono : MonoBehaviour
    {
        [SerializeField] private LeanTweenAnimationSpec animationSpec;
        [SerializeField] private float dropdownHeightDifference = 50f;
        private RectTransform _rectTransform;
        private float _noDropdownHeight;
        private float _withDropdownHeight;
        private bool _dropdownIsActive;

        private event Action onExpand;
        private event Action onHide;
        
        private float AnimationLength
        {
            get
            {
                if (animationSpec == null) return 0.6f;
                else return animationSpec.GetAnimationLength();
            }
        }
        private LeanTweenType AnimationEaseType
        {
            get
            {
                if (animationSpec == null) return LeanTweenType.easeOutCubic;
                else return animationSpec.GetEaseType();
            }
        }
        
        private void OnValidate()
        {
            dropdownHeightDifference = Mathf.Clamp(dropdownHeightDifference, 0f, 1080f);
        }

        private void OnDisable()
        {
            SnapHide();
        }

        private bool _isInit;
        
        protected void InitializeDropdown()
        {
            InitializeRectTransform();
            InitializeHeights();
            _isInit = true;
        }
        
        void InitializeHeights()
        {
            _noDropdownHeight = _rectTransform.sizeDelta.y;
            _withDropdownHeight = _noDropdownHeight + dropdownHeightDifference;
        }
        void InitializeRectTransform()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.Require(this);
        }

        bool CheckInit()
        {
            if (!_isInit) Debug.LogWarning("Initialize the dropdown before using it!");
            return _isInit;
        }
        
        public void ToggleDropdown()
        {
            if(!CheckInit()) return;
            
            if(_dropdownIsActive) HideDropdown();
            else ExpandDropdown();
        }

        public void HideDropdown()
        {
            if(!_dropdownIsActive) return;
            TweenDropdown(_noDropdownHeight);
            _dropdownIsActive = false;
            InvokeOnHide();
        }
        
        private void ExpandDropdown()
        {
            if(_dropdownIsActive) return;
            TweenDropdown(_withDropdownHeight);
            _dropdownIsActive = true;
            InvokeOnExpand();
        }

        private void TweenDropdown(float targetY, Action onComplete = null)
        {
            LeanTween.cancel(gameObject);
            LeanTween.size(_rectTransform, new Vector2(_rectTransform.sizeDelta.x, targetY),
                    AnimationLength)
                .setOnComplete(onComplete)
                .setEase(AnimationEaseType);
        }
        private void SnapTweenDropdown(float targetY)
        {
            LeanTween.cancel(gameObject);
            LeanTween.size(_rectTransform, new Vector2(_rectTransform.sizeDelta.x, targetY),
                    0.08f)
                .setEase(AnimationEaseType);
        }

        public void ListenForOnExpand(Action func) => onExpand += func;
        public void ListenForOnHide(Action func) => onHide += func;
        private void InvokeOnExpand() => onExpand?.Invoke();
        private void InvokeOnHide() => onHide?.Invoke();

        public void SnapHide()
        {
            if(!_dropdownIsActive) return;
            SnapTweenDropdown(_noDropdownHeight);
            _dropdownIsActive = false;
        }
    }
}