using UnityEngine;

namespace FitnessApp.UICore.MovableElementList
{
    public class TouchDragInput : MonoBehaviour, IDragInput
    {
        private Vector2 _lastTouchPosition = Vector2.zero;

        public bool CanGetInput => Input.touchCount > 0;
        
        private void LateUpdate()
        {
            _lastTouchPosition = Input.touches[0].position;
        }
        
        public Vector2 GetElementTargetPosition(RectTransform rect)
        {
            var currentTouchPosition = Input.touches[0].position;
            var diff = currentTouchPosition - _lastTouchPosition;
            var rectPos = rect.position;

            var newPosition = rectPos + new Vector3(diff.x, diff.y, rectPos.z);
            return newPosition;  
        }
    }
}