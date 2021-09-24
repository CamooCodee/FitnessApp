using FitnessAppAPI;
using UnityEngine;
using UnityEngine.Events;

namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public class ExerciseElementFactory : MonoBehaviour, IExerciseElementFactory
    {
        [Space(10f)]
        [SerializeField] private GameObject elementPrefab;

        [SerializeField] private UnityEvent<int> onDeleteExercise;
        [SerializeField] private UnityEvent<int> onEditExercise;
        [SerializeField] private UnityEvent<int> onCopyExercise;
        
        private void Awake() => Setup();

        protected virtual void Setup() => elementPrefab.Require(this);

        public virtual ExerciseElement InstantiateElement(ExerciseData data, Transform elementContainer)
        {
            var instance = Instantiate(elementPrefab, elementContainer);
            var element = instance.GetComponent<ExerciseElement>();
            if (element == null)
            {
                Debug.LogWarning($"The element must have a '{nameof(ExerciseElement)}' component attached to it.");
                Destroy(instance);
                return null;
            }
            
            element.SetData(data);
            element.ListenForDelete(onDeleteExercise);
            element.ListenForEdit(onEditExercise);
            element.ListenForCopy(onCopyExercise);

            return element;
        }
    }
}