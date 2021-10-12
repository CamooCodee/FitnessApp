using FitnessAppAPI;
using UnityEngine;
using UnityEngine.UI;

namespace FitnessApp.UIConcretes.FollowAlong.Logic
{
    public class PreviousButtonEnableBehaviour : MonoBehaviour, IFollowAlongListener
    {
        [SerializeField] private Button button;
        [SerializeField] private GameObject overlay;

        private void Awake()
        {
            overlay.Require(this);
            button.Require(this);
        }

        public void OnNewElement(IWorkoutDataElement[] elements, int current)
        {
            overlay.SetActive(current == 0);
            button.interactable = current != 0;
        }

        public void ResetListener()
        {
            
        }
    }
}