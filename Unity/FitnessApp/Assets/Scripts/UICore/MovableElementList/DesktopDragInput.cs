using System;
using UnityEngine;

namespace FitnessApp.UICore.MovableElementList
{
    public class DesktopDragInput : MonoBehaviour, IDragInput
    {
        private Vector2 _lastMousePosition = Vector2.zero;

        public bool CanGetInput => true;

        private void LateUpdate()
        {
            _lastMousePosition = Input.mousePosition;
        }

        public Vector2 GetElementTargetPosition(RectTransform rect)
        {
            Vector2 currentMousePosition = Input.mousePosition;
            var diff = currentMousePosition - _lastMousePosition;
            var rectPos = rect.position;

            var newPosition = rectPos + new Vector3(diff.x, diff.y, rectPos.z);
            return newPosition;
        }
    }
}