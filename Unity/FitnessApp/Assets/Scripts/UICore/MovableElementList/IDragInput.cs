using UnityEngine;

namespace FitnessApp.UICore.MovableElementList
{
    public interface IDragInput
    {
        bool CanGetInput { get; }
        Vector2 GetElementTargetPosition(RectTransform rect);
    }
}