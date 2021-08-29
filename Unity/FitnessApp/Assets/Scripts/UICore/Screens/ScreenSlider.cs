using System;
using System.Collections.Generic;
using UnityEngine;

namespace FitnessApp.UICore.Screens
{
    [AddComponentMenu("Screen Slider")]
    public class ScreenSlider : MonoBehaviour
    {
        [SerializeField] private LeanTweenAnimationSpec animationSpec;
        [SerializeField] private Transform slideOrigin;
        [SerializeField] private Transform slideTarget;
        
        private float AnimationLength
        {
            get
            {
                if (animationSpec == null) return 0.4f;
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

        private Stack<GameObject> _currentScreenObjects = new Stack<GameObject>();

        private void OnValidate()
        {
            if (slideTarget == null) slideTarget = transform;
        }

        public void SlideAScreenIn(GameObject screenObject)
        {
            if (screenObject == null)
            {
                Debug.LogWarning("Can't slide a null screen!");
                return;
            }

            if (!_currentScreenObjects.Contains(screenObject))
            {
                _currentScreenObjects.Push(screenObject);
                SlideIn(screenObject);
                if (screenObject.TryGetComponent(out Screen screen)) screen.Open();
                else Debug.Log("Sliding in a screen without a Screen component!");
            }
            else Debug.LogWarning("Can't slide screen. It's already inside of the screens stack.");
        }

        public void SlideAScreenOut()
        {
            bool atLeastOneScreenInStack = _currentScreenObjects.Count > 0;
            if (atLeastOneScreenInStack)
            {
                var screen = _currentScreenObjects.Peek();
                SlideOut();
            }
        }

        void SlideIn(GameObject screen)
        {
            if(screen == null) return;

            var originPosition = slideOrigin.position;
            
            screen.transform.position = originPosition;
            LeanTween.cancel(screen);
            LeanTween.move(screen, slideTarget.position, AnimationLength)
                .setEase(AnimationEaseType);
                
            screen.transform.SetAsLastSibling();
        }

        void SlideOut()
        {
            var toSlideOut = _currentScreenObjects.Pop();
            
            LeanTween.cancel(toSlideOut);
            LeanTween.move(toSlideOut, slideOrigin.position, AnimationLength)
                .setEase(AnimationEaseType)
                .setOnComplete((Action) delegate { OnSlideOutComplete(toSlideOut.transform); });
        }
        
        void OnSlideOutComplete(Transform t)
        {
            SetAsFirstChild(t);    
            InvokeCloseOnScreen(t.gameObject);
        }
        
        void InvokeCloseOnScreen(GameObject screenObject)
        {
            if (screenObject.TryGetComponent(out Screen screen)) screen.Close();
            else Debug.Log("Sliding out a screen without a Screen component!");
        }

        void SetAsFirstChild(Transform t)
        {
            if(t != null)
                t.SetAsFirstSibling();
        }
    }
}