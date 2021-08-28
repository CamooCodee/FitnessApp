using FitnessApp.UICore;
using UnityEngine;

namespace FitnessApp.UICore.UITabs
{
    [AddComponentMenu("Ui Tabs/Hover Object - Tabs")]
    public class HoverObjectResponseTabSelection : MonoBehaviour, ITabSelectionListener
    {
        [Space(10f)]
        [Header("Required")]
        [SerializeField] private TabGroup tabGroup;
        [SerializeField] private Transform[] positions;
        [SerializeField] private GameObject hoverObject;
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
        private int _currentlySelected = -1;
        
        private void Awake()
        {
            tabGroup.Require(this);
            hoverObject.Require(this);
            tabGroup.AddListener(this);
        }

        public void SelectTab(int index)
        {
            if (!index.IsValidIndexFor(positions))
            {
                Debug.LogWarning($"'{index}' is invalid for the screen list with a length of {positions.Length}.");
                return;
            }
            
            MoveObjectToFitSelection(index);
        }

        void MoveObjectToFitSelection(int selection)
        {
            if(_currentlySelected == selection) return;
            var targetPos = positions[selection].transform.position;
            
            LeanTween.cancel(hoverObject);
            LeanTween.move(hoverObject, targetPos, AnimationLength).setEase(AnimationEaseType);
            
            _currentlySelected = selection;
        }
    }
}