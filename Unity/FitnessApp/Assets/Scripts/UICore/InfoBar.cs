using System;
using TMPro;
using UnityEngine;

namespace FitnessApp.UICore
{
    public class InfoBar : MonoBehaviour
    {
        public static InfoBar instance;

        [SerializeField] private LeanTweenAnimationSpec animationSpec;
        [SerializeField] private GameObject toAnimate;
        [SerializeField] private TextMeshProUGUI display;

        [SerializeField] private float expansionTime;
        [SerializeField] private bool stretchTimeByContentSize = true;
        private const float STRETCH_SENSITIVITY = 0.025f;
        
        private float _originalX;
        private float _originalExpansionTime;

        
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
            expansionTime = Mathf.Max(expansionTime, 0f);
        }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(this);
                return;
            }

            toAnimate.Require(this);
            display.Require(this);
            _originalExpansionTime = expansionTime;
        }

        private void Start()
        {
            _originalX = toAnimate.transform.position.x; // Position in Awake is wrong value
        }

        public void Display(string content)
        {
            display.text = content;
            if (stretchTimeByContentSize)
            {
                // Not more than double the original time
                expansionTime = _originalExpansionTime + Mathf.Clamp(content.Length * STRETCH_SENSITIVITY, 0f, _originalExpansionTime);
            }
            LeanTween.cancel(toAnimate);
            toAnimate.transform.position = new Vector2(_originalX, toAnimate.transform.position.y);
            Tween();
        }

        void Tween()
        {
            LeanTween.moveX(toAnimate, transform.position.x, AnimationLength)
                .setEase(AnimationEaseType)
                .setOnComplete(TweenOut);
        }
        
        
        void TweenOut()
        {
            LeanTween.moveX(toAnimate, _originalX, AnimationLength)
                .setEase(AnimationEaseType)
                .setDelay(expansionTime);
        }
    }
}