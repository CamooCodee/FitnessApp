using UnityEngine;

namespace FitnessApp.UICore
{
    [CreateAssetMenu(fileName = "NewAnimationSpec", menuName = "Scriptable Objects/Animation Specification")]
    public class LeanTweenAnimationSpec : ScriptableObject
    {
        [SerializeField] private float animationLength = 0.5f;
        [SerializeField] private LeanTweenType easeType = LeanTweenType.easeOutCubic;

        private void OnValidate()
        {
            animationLength = Mathf.Clamp(animationLength, 0f, 100f);
        }

        public float GetAnimationLength() => animationLength;
        public LeanTweenType GetEaseType() => easeType;
    }
}