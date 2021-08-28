using UnityEngine;

namespace FitnessApp.UICore.UITabs
{
    [AddComponentMenu("Ui Tabs/Screens - Tabs")]
    public class ScreenResponseTabSelection : MonoBehaviour, ITabSelectionListener
    {
        [Space(10f)]
        [Header("Required")]
        [SerializeField] private TabGroup tabGroup;
        [SerializeField] private GameObject[] screens;
        [SerializeField] private Transform selectedScreenPosition;
        [Space(10f)]
        [Header("Optional"), Space(20f)]
        [SerializeField] private LeanTweenAnimationSpec animationSpec;

        
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
        
        private void Awake()
        {
            tabGroup.Require(this);
            selectedScreenPosition.Require(this);
            tabGroup.AddListener(this);
        }
        
        public void SelectTab(int index)
        {
            if (!index.IsValidIndexFor(screens))
            {
                Debug.LogWarning($"The index '{index}' is invalid for the screen list with a length of {screens.Length}.");
                return;
            }

            MoveScreensToFitSelection(index);
        }

        void MoveScreensToFitSelection(int selection)
        {
            var screen = screens[selection];

            var movement = CalculateMovement(screen);
            if(movement == Vector3.zero) return;            
            for (var i = 0; i < screens.Length; i++)
            {
                LeanTween.cancel(screens[i]);
                LeanTween.move(screens[i], screens[i].transform.position + movement, AnimationLength)
                    .setEase(AnimationEaseType);
            }
        }
        
        Vector3 CalculateMovement(GameObject screen)
        {
            Vector2 movementV2 = selectedScreenPosition.position - screen.transform.position;
            return new Vector3(movementV2.x, movementV2.y, 0f);
        }
    }
}