using FitnessApp.UICore;
using UnityEngine;
using UnityEngine.UI;

namespace FitnessApp.Animations
{
    public class AddExerciseAnimation : MonoBehaviour
    {
        [Header("Animation Specifications")]
        [SerializeField] private LeanTweenAnimationSpec slideAnimationSpec;
        [SerializeField] private LeanTweenAnimationSpec fadeAnimationSpec;
        [Header("References")]
        [SerializeField] private RectTransform infoBox;
        [SerializeField] private RectTransform replacementBox;
        [SerializeField] private RectTransform elementRect;

        private float SlideAnimationLength => slideAnimationSpec == null ? 0.6f : slideAnimationSpec.GetAnimationLength();
        private float FadeAnimationLength => slideAnimationSpec == null ? 0.6f : slideAnimationSpec.GetAnimationLength();
        private LeanTweenType SlideAnimationEaseType => fadeAnimationSpec == null ? LeanTweenType.easeOutCubic : fadeAnimationSpec.GetEaseType();
        private LeanTweenType FadeAnimationEaseType => fadeAnimationSpec == null ? LeanTweenType.easeOutCubic : fadeAnimationSpec.GetEaseType();

        private Vector2 _replacementBoxStartPos;
        private Vector2 _infoBoxStartPos;
        
        private void Start()
        {
            infoBox.Require(this);
            replacementBox.Require(this);
            elementRect.Require(this);

            _infoBoxStartPos = infoBox.anchoredPosition;
            _replacementBoxStartPos = replacementBox.anchoredPosition;
        }

        public void Animate()
        {
            MoveInfoBox();
            MoveReplacementBox();
        }

        void MoveInfoBox()
        {
            LeanTween.cancel(infoBox.gameObject);
            var anchPos = infoBox.anchoredPosition;
            var targetX = anchPos.x - infoBox.rect.width;
            var targetPos = new Vector2(targetX, anchPos.y);

            LeanTween.move(infoBox, targetPos, SlideAnimationLength).setEase(SlideAnimationEaseType).setOnComplete(ScaleUpInfoBox);
        }
        void MoveReplacementBox()
        {
            LeanTween.cancel(replacementBox.gameObject);
            ResetReplacementBox();
            var anchPos = replacementBox.anchoredPosition;
            var targetX = anchPos.x - elementRect.rect.width;
            var targetPos = new Vector2(targetX, anchPos.y);

            LeanTween.move(replacementBox, targetPos, SlideAnimationLength).setEase(SlideAnimationEaseType).setOnComplete(FadeOutReplacementBox);
        }

        void FadeOutReplacementBox()
        {
            LeanTween.alpha(replacementBox, 0f, FadeAnimationLength).setOnComplete(ResetReplacementBox).setEase(FadeAnimationEaseType);
        }
        void ScaleUpInfoBox()
        {
            infoBox.anchoredPosition = _infoBoxStartPos;
            var scale = infoBox.localScale;
            var previousSize = Vector3.one;
            infoBox.localScale = new Vector3(scale.x * 0.98f, scale.y * 0.98f, scale.z);
            LeanTween.scale(infoBox, previousSize, FadeAnimationLength).setEase(FadeAnimationEaseType);
        }
        
        void ResetReplacementBox()
        {
            replacementBox.anchoredPosition = _replacementBoxStartPos;
            var image = replacementBox.GetComponent<Image>();
            var c = image.color;
            image.color = new Color(c.r, c.g, c.b, 1f);
        }
    }
}