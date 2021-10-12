using TMPro;
using UnityEngine;

namespace FitnessApp.UICore.MovableElementList
{
    public class TouchDragInput : MonoBehaviour, IDragInput
    {
        private TextMeshProUGUI _debugText;
        
        private Vector2 _lastTouchPosition = Vector2.zero;

        public bool CanGetInput => Input.touchCount > 0;

        private void Start()
        {
            _debugText = GameObject.Find("TouchDebugDisplay").GetComponent<TextMeshProUGUI>();
        }

        private void LateUpdate()
        {
            if(!CanGetInput) return;
            
            _lastTouchPosition = Input.touches[0].position;
        }
        
        public Vector2 GetElementTargetPosition(RectTransform rect)
        {
            var currentTouchPosition = Input.touches[0].position;
            var diff = currentTouchPosition - _lastTouchPosition;
            if(_lastTouchPosition == Vector2.zero) diff = Vector2.zero;
            var rectPos = rect.position;

            var newPosition = rectPos + new Vector3(diff.x, diff.y, rectPos.z);
            _debugText.text = ((Vector2)currentTouchPosition).ToString();
            return newPosition;
        }
    }
}