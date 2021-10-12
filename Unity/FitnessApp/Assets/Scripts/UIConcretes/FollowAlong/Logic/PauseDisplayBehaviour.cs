using FitnessAppAPI;
using UnityEngine;

namespace FitnessApp.UIConcretes.FollowAlong.Logic
{
    public class PauseDisplayBehaviour : MonoBehaviour, IFollowAlongListener
    {
        [SerializeField] private RectTransform timer;
        [SerializeField] private RectTransform timerRect;
        [SerializeField] private Transform pauseCard;

        private void Awake()
        {
            pauseCard.Require(this);
        }

        public void OnNewElement(IWorkoutDataElement[] elements, int current)
        {
            var element = elements[current];
            if (element == null || !(element is PauseData pauseData))
            {
                pauseCard.SetAsFirstSibling();
                return;
            }
            pauseCard.SetAsLastSibling();
            timer.gameObject.SetActive(true);
            timer.SetParent(timerRect);
            timer.sizeDelta = timerRect.sizeDelta;
            timer.position = timerRect.position;
        }

        public void ResetListener()
        {
            
        }
    }
}