using System.Collections;
using FitnessAppAPI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FitnessApp.UIConcretes.FollowAlong.Logic
{
    public class StepDisplayBehaviour : MonoBehaviour, IFollowAlongListener
    {
        [SerializeField] private Transform centre;
        [SerializeField] private Transform stepSquareHolder;
        private HorizontalOrVerticalLayoutGroup _layoutGroup;
        [Space(20f)]
        [SerializeField] private Sprite exerciseIcon;
        [SerializeField] private Sprite pauseIcon;
        [SerializeField] private GameObject completedSquare;
        [SerializeField] private GameObject currentSquare;
        [SerializeField] private GameObject comingUpSquare;
        [SerializeField] private LeanTweenType easeType;
        [SerializeField] private float animLength = 0.35f;
        [Space(20f)]
        [SerializeField] private TextMeshProUGUI stepTextDisplay;
        
        private StepSquare[] _currentSquares;

        private bool _wasInit = false;
        private int _prevCurrent;
        
        private void Awake()
        {
            centre.Require(this);
            stepSquareHolder.Require(this);
            completedSquare.Require(this);
            currentSquare.Require(this);
            comingUpSquare.Require(this);
            stepTextDisplay.Require(this);
            
            _layoutGroup = stepSquareHolder.GetComponent<HorizontalOrVerticalLayoutGroup>();
            _layoutGroup.Require(this);
            for (int i = 0; i < stepSquareHolder.childCount; i++) 
                Destroy(stepSquareHolder.GetChild(i).gameObject);
        }

        public void OnNewElement(IWorkoutDataElement[] elements, int current)
        {
            stepTextDisplay.text = $"Step: {current + 1}/{elements.Length}";
            
            if (current == 0 && !_wasInit)
            {
                _wasInit = true;
                InstantiateSquares(elements);
                return;
            }

            if (_layoutGroup.enabled) _layoutGroup.enabled = false;
            
            if(current > 0) _currentSquares[current - 1].SetSquareColorByPrefab(completedSquare, animLength);
            _currentSquares[current].SetSquareColorByPrefab(currentSquare, animLength);
            if(_prevCurrent > current)
                _currentSquares[_prevCurrent].SetSquareColorByPrefab(comingUpSquare, animLength);
            
            var offset = _currentSquares[1].transform.position - _currentSquares[0].transform.position;
            
            for (var i = 0; i < _currentSquares.Length; i++)
            {
                var pos = centre.position + (i - current) * offset;
                
                var t = _currentSquares[i].transform;
                LeanTween.move(t.gameObject, pos, animLength).setEase(easeType);
            }

            _prevCurrent = current;
        }

        void InstantiateSquares(IWorkoutDataElement[] elements)
        {
            Instantiate(currentSquare, stepSquareHolder);
            
            for (var i = 1; i < elements.Length; i++) 
                Instantiate(comingUpSquare, stepSquareHolder);

            _currentSquares = stepSquareHolder.GetComponentsInChildren<StepSquare>();
            
            for (var i = 0; i < _currentSquares.Length; i++) 
                _currentSquares[i].SetIcon(GetIconByData(elements[i]));

            StartCoroutine(AlignSquares());
        }

        IEnumerator AlignSquares()
        {
            yield return new WaitForEndOfFrame();
            
            _layoutGroup.enabled = false;

            var offset = _currentSquares[1].transform.position - _currentSquares[0].transform.position;
            
            for (var i = 0; i < _currentSquares.Length; i++)
            {
                var pos = centre.position + i * offset;
                
                var t = _currentSquares[i].transform; 
                t.position = pos;
            }
        }

        public void ResetListener()
        {
            _wasInit = false;
            _prevCurrent = 0;
            for (int i = 0; i < stepSquareHolder.childCount; i++) 
                Destroy(stepSquareHolder.GetChild(i).gameObject);
            
            _layoutGroup.enabled = true;
        }

        private Sprite GetIconByData(IWorkoutDataElement data)
        {
            if (data is PauseData) return pauseIcon;
            return exerciseIcon;
        }
    }
}