using System;
using System.Collections.Generic;
using UnityEngine;

namespace FitnessApp.UICore
{
    [AddComponentMenu("Screen Slider")]
    public class ScreenSlideResponse : MonoBehaviour
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
            }
            else Debug.LogWarning("Can't slide screen. It's already inside of the screens stack.");
        }

        public void SlideAScreenOut()
        {
            if (_currentScreenObjects.Count > 0) SlideOut();
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
                .setOnComplete((Action) delegate { SetAsFirstChild(toSlideOut.transform); });
        }

        void SetAsFirstChild(Transform t)
        {
            if(t != null)
                t.SetAsFirstSibling();
        }
    }
}