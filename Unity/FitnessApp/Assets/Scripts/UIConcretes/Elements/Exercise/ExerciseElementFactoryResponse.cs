using FitnessAppAPI;
using UnityEngine;
using UnityEngine.Events;

namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public class ExerciseElementFactoryResponse : MonoBehaviour, IExerciseElementFactory
    {
        [Space(10f)]
        [SerializeField] private GameObject elementPrefab;

        [SerializeField] private UnityEvent<int> onDeleteExercise;
        [SerializeField] private UnityEvent<int> onEditExercise;
        [SerializeField] private UnityEvent<int> onCopyExercise;
        

        private void Awake()
        {
            elementPrefab.Require(this);
        }

        public void InstantiateElement(ExerciseData data, Transform elementContainer)
        {
            var instance = Instantiate(elementPrefab, elementContainer);
            var element = instance.GetComponent<ExerciseElement>();
            if (element == null)
            {
                Debug.LogWarning($"The element must have a '{nameof(ExerciseElement)}' component attached to it.");
                Destroy(instance);
                return;
            }
            
            element.SetData(data);
            element.ListenForDelete(onDeleteExercise);
            element.ListenForEdit(onEditExercise);
            element.ListenForCopy(onCopyExercise);
        }
    }
}