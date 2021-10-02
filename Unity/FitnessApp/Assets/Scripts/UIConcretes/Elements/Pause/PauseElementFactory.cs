using FitnessAppAPI;
using UnityEngine;
using UnityEngine.Events;

namespace UIConcretes.Elements.Pause
{
    public class PauseElementFactory : MonoBehaviour, IPauseElementFactory
    {
        [SerializeField] private GameObject elementPrefab;
        [SerializeField] private UnityEvent<int> onCopyPause;
        [SerializeField] private UnityEvent<int> onDeletePause;


        public PauseElement InstantiateElement(PauseData data, Transform elementContainer)
        {
            var instance = Instantiate(elementPrefab, elementContainer);

            if (!instance.TryGetComponent(out PauseElement element))
            {
                Debug.LogWarning("You're not instantiating pause elements with a pause element factory.");
                return null;
            }

            element.ListenForCopy(onCopyPause);
            element.ListenForDelete(onDeletePause);
            element.Length = data.length;
            
            return element;
        }
    }
}