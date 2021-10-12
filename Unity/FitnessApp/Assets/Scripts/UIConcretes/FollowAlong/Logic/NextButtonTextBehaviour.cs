using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.FollowAlong.Logic
{
    public class NextButtonTextBehaviour : MonoBehaviour, IFollowAlongListener
    {
        [SerializeField] private TextMeshProUGUI display;

        private void Awake()
        {
            display.Require(this);
        }

        public void OnNewElement(IWorkoutDataElement[] elements, int current)
        {
            bool isLastElement = current == elements.Length - 1;
            if (isLastElement)
            {
                display.text = "Finish";
                return;
            }
            
            var nextElement = elements[current + 1];
            if(nextElement == null) return;

            if (nextElement is PauseData pause)
                display.text = $"Next: Pause for {pause.length} s";
            if (nextElement is OffsetExerciseData ex)
                display.text = $"Next: {ex.name}";
        }

        public void ResetListener()
        {
            display.text = "- blank -";
        }
    }
}