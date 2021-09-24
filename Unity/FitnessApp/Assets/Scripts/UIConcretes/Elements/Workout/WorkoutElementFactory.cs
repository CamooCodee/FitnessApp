using FitnessAppAPI;
using UnityEngine;
using UnityEngine.Events;

namespace UIConcretes.Elements.Workout
{
    public class WorkoutElementFactory : MonoBehaviour, IWorkoutElementFactory
    {
        [SerializeField] private GameObject elementPrefab;
        
        [SerializeField] private UnityEvent<int> onDeleteWorkout;
        [SerializeField] private UnityEvent<int> onEditWorkout;
        [SerializeField] private UnityEvent<int> onCopyWorkout;
        
        public WorkoutElement InstantiateElement(WorkoutData data, Transform elementContainer)
        {
            var instance = Instantiate(elementPrefab, elementContainer);
            var element = instance.GetComponent<WorkoutElement>();
            if (element == null)
            {
                Debug.LogWarning($"Instantiating workout elements using a prefab that doesn't have a {nameof(WorkoutData)} component.");
                return null;
            }
            
            element.SetData(data);
            element.ListenForCopy(onCopyWorkout);
            element.ListenForEdit(onEditWorkout);
            element.ListenForDelete(onDeleteWorkout);

            return element;
        }
    }
}