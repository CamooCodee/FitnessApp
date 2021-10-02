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
        [SerializeField] private Transform homeScreen;
        
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

        private readonly Stack<GameObject> _currentScreenObjects = new Stack<GameObject>();

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
            if (_currentScreenObjects.Count <= 0) return;
            
            SlideOut();
        }

        void SlideIn(GameObject screen)
        {
            if(screen == null) return;
            screen.SetActive(true);
            var originPosition = slideOrigin.position;
            
            screen.transform.position = originPosition;
            LeanTween.cancel(screen);
            LeanTween.move(screen, slideTarget.position, AnimationLength)
                .setEase(AnimationEaseType)
                .setOnComplete(OnSlideInComplete);
                
            screen.transform.SetAsLastSibling();
        }

        void SlideOut()
        {
            var toSlideOut = _currentScreenObjects.Pop();
            var nextScreen = GetNextScreen();
            if(nextScreen != null) nextScreen.SetActive(true);
            
            InvokeReopenEventOnScreen(nextScreen);
            
            LeanTween.cancel(toSlideOut);
            LeanTween.move(toSlideOut, slideOrigin.position, AnimationLength)
                .setEase(AnimationEaseType)
                .setOnComplete((Action) delegate { OnSlideOutComplete(toSlideOut.transform); });
        }
        
        void OnSlideOutComplete(Transform t)
        {
            var nextScreen = GetNextScreen();
            if(nextScreen != null) nextScreen.transform.SetAsLastSibling();
            DisableAllApartFromLastSibling();
            InvokeCloseEventOnScreen(t.gameObject);
        }

        void OnSlideInComplete()
        {
            DisableAllApartFromLastSibling();
        }
        
        void InvokeCloseEventOnScreen(GameObject screenObject)
        {
            if (screenObject.TryGetComponent(out Screen screen)) screen.Close();
            else Debug.Log("Sliding out a screen without a Screen component!");
        }

        void InvokeReopenEventOnScreen(GameObject screenGo)
        {
            if (screenGo.TryGetComponent(out Screen screen)) screen.ReOpen();
        }
        
        void DisableAllApartFromLastSibling()
        {
            var count = transform.childCount;
            if(count <= 1) return;
            
            for (int i = 0; i < count - 1; i++)
            {
                var go = transform.GetChild(i).gameObject;
                go.SetActive(false);
            }

            var last = transform.GetChild(count - 1).gameObject;
            last.SetActive(true);
        }

        GameObject GetNextScreen()
        {
            if(_currentScreenObjects.Count > 0)
                return _currentScreenObjects.Peek();
            
            return homeScreen != null ? homeScreen.gameObject : null;
        }
    }
}