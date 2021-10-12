using UnityEngine;
using UnityEngine.EventSystems;

namespace FitnessApp.UICore.MovableElementList
{
    public class MovableTrigger : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private MovableElement targetElement;
        private IStopDragBehaviour _stopDragBehaviour;

        private void Awake()
        {
            targetElement.Require(this);
            _stopDragBehaviour = GetComponent<IStopDragBehaviour>();
            _stopDragBehaviour.Require(this);
            _stopDragBehaviour.SetEnabled(false);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            targetElement.OnBeginDrag();
            _stopDragBehaviour.SetEnabled(true);
            _stopDragBehaviour.ListenForStop(OnPointerUp);
        }

        private void OnPointerUp()
        {
            targetElement.OnEndDrag();
            _stopDragBehaviour.StopListeningForStop(OnPointerUp);
            _stopDragBehaviour.SetEnabled(false);
        }
    }
}